using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hire.Core.Data;
using Hire.Core.Domain.Orders;
using Hire.Core.Domain.Rentals;
using Hire.Core.Domain.Returns;
using Hire.Services.Converter;
using Hire.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace Hire.Services.Orders
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<OrderEntity> _orderRepository;
        private readonly IRepository<OrderItemEntity> _orderItemRepository;
        private readonly IRepository<VehicleEntity> _vehicleRepository;
        private readonly IRepository<RentalStateEntity> _rentalStateRepository;
        private readonly ICurrencyConverter _currencyConverter;
        private readonly IConfigurationProvider _mappingConfiguration;

        public OrderService(
            IRepository<OrderEntity> orderRepository,
            IRepository<OrderItemEntity> orderItemRepository,
            IRepository<VehicleEntity> vehicleRepository, 
            IRepository<RentalStateEntity> rentalStateRepository,
            ICurrencyConverter currencyConverter,
            IConfigurationProvider mappingConfiguration)
        {
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
            _vehicleRepository = vehicleRepository;
            _rentalStateRepository = rentalStateRepository;
            _currencyConverter = currencyConverter;
            _mappingConfiguration = mappingConfiguration;
        }

        public async Task<PagedResults<Order>> GetAllOrdersAsync(PagingOptions pagingOptions)
        {
            var query = _orderRepository
                .Table;

            var size = await query.CountAsync();

            var items = await query
                .Skip(pagingOptions.Offset!.Value)
                .Take(pagingOptions.Limit!.Value)
                .ProjectTo<Order>(_mappingConfiguration)
                .ToArrayAsync();

            return new PagedResults<Order>
            {
                Items = items,
                TotalSize = size
            };
        }

        public async Task<PagedResults<Order>> GetOrdersAsync(int userId, PagingOptions pagingOptions)
        {
            var query = _orderRepository
                .Table
                .Where(x => x.UserId == userId);

            var size = await query.CountAsync();

            var items = await query
                .Skip(pagingOptions.Offset!.Value)
                .Take(pagingOptions.Limit!.Value)
                .ProjectTo<Order>(_mappingConfiguration)
                .ToArrayAsync();

            return new PagedResults<Order>
            {
                Items = items,
                TotalSize = size
            };
        }

        public Task<int> BeginOrderAsync(int userId)
        {
            var order = _orderRepository
                .Table
                .FirstOrDefault(x => x.UserId == userId && x.Status == OrderStatus.Pending || x.Status == OrderStatus.Processing);
            if (order != null)
            {
                return Task.FromResult(order.Id);
            }

            var orderEntity = new OrderEntity
            {
                CreatedOn = DateTimeOffset.Now,
                Status = OrderStatus.Pending,
                UserId = userId
            };

            _orderRepository.Insert(orderEntity);

            return Task.FromResult(orderEntity.Id);
        }

        public async Task<int> LeaseAsync(int orderId, int rentalId, DateTimeOffset start, DateTimeOffset end)
        {
            var oiEntity = _orderItemRepository
                .Table
                .FirstOrDefault(x => x.OrderId == orderId && x.RentalId == rentalId);

            var rental = _vehicleRepository.GetById(rentalId);

            if (oiEntity == null)
            {
                var oi = new OrderItemEntity
                {
                    OrderId = orderId,
                    RentalId = rentalId,
                    StartAt = start,
                    EndAt = end,
                    DailyPrice = rental.Price,
                    Quantity = 1,
                    TaxRate = 19m
                };

                _orderItemRepository.Insert(oi);

                RecalculateOrder(orderId);

                return await Task.FromResult(oi.Id);
            }

            oiEntity.StartAt = start;
            oiEntity.EndAt = end;

            _orderItemRepository.Update(oiEntity);

            RecalculateOrder(orderId);

            return await Task.FromResult(oiEntity.Id);
        }

        private void RecalculateOrder(int orderId)
        {
            var order = _orderRepository.GetById(orderId);

            if (order.Status == OrderStatus.Cancelled || order.Status == OrderStatus.Complete)
            {
                return;
            }

            order.UpdatedOn = DateTimeOffset.Now;

            var items = _orderItemRepository
                .Table
                .Where(x => x.OrderId == orderId)
                .ToList();

            decimal subtotal = 0.0m;
            foreach (var itemEntity in items)
            {
                var minutes = (decimal)(itemEntity.EndAt - itemEntity.StartAt).TotalMinutes;

                subtotal += ((itemEntity.DailyPrice * minutes) / 1400m) + itemEntity.AdditionalCost.GetValueOrDefault(0);
            }

            order.Subtotal = subtotal;
            order.Total = order.Subtotal - (order.Subtotal * order.Discount / 100);

            if (items.All(x => x.ItemStatus == OrderItemStatus.Released))
            {
                order.Status = OrderStatus.Complete;
            }

            _orderRepository.Update(order);
        }

        public async Task<int> ReleaseAsync(int orderId, int rentalId, ReleaseVehicleForm releaseForm)
        {
            var oiEntity = _orderItemRepository
                .Table
                .FirstOrDefault(x => x.OrderId == orderId && x.RentalId == rentalId);

            if (oiEntity == null || oiEntity.ItemStatus == OrderItemStatus.Released)
            {
                return 0;
            }

            var state = new VehicleStateEntity
            {
                IsTankFull = releaseForm.IsTankFull,
                Odometer = releaseForm.Odometer
            };

            if (oiEntity.StateId.HasValue)
            {
                var stateEntity = _rentalStateRepository.GetById(oiEntity.StateId);
                _rentalStateRepository.Delete(stateEntity);
            }

            oiEntity.State = state;
            oiEntity.ItemStatus = OrderItemStatus.Released;

            if (!state.IsTankFull)
            {
                oiEntity.AdditionalCost = 100;
            }

            _orderItemRepository.Update(oiEntity);

            RecalculateOrder(orderId);

            return await Task.FromResult(oiEntity.Id);
        }

        public async Task<int> CompleteOrderAsync(int orderId)
        {
            var oiEntities = _orderItemRepository
                .Table
                .Where(x => x.OrderId == orderId && x.ItemStatus != OrderItemStatus.Released)
                .ToList();

            foreach (var itemEntity in oiEntities)
            {
                if (itemEntity.StateId.HasValue)
                {
                    var stateEntity = _rentalStateRepository.GetById(itemEntity.StateId);
                    _rentalStateRepository.Delete(stateEntity);
                }

                itemEntity.ItemStatus = OrderItemStatus.Released;

                _orderItemRepository.Update(itemEntity);
            }

            RecalculateOrder(orderId);

            return await Task.FromResult(orderId);
        }

        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            var order = await _orderRepository
                .Table
                .Where(x => x.Id == orderId)
                .FirstOrDefaultAsync();

            return _mappingConfiguration.CreateMapper().Map<Order>(order);
        }
    }
}
