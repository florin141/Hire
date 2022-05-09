using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
        private readonly ICurrencyConverter _currencyConverter;
        private readonly IMapper _mapper;

        public OrderService(
            IRepository<OrderEntity> orderRepository,
            IRepository<OrderItemEntity> orderItemRepository,
            IRepository<VehicleEntity> vehicleRepository, 
            ICurrencyConverter currencyConverter,
            IMapper mapper)
        {
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
            _vehicleRepository = vehicleRepository;
            _currencyConverter = currencyConverter;
            _mapper = mapper;
        }

        public async Task<ICollection<Order>> GetOrdersAsync(int userId)
        {
            var orderEntities = await _orderRepository
                .Table
                .Where(x => x.UserId == userId)
                .ToListAsync();

            var orders = _mapper.Map<List<Order>>(orderEntities);

            return orders;
        }

        public Task<int> BeginOrderAsync(int userId)
        {
            var order = new OrderEntity
            {
                CreatedOn = DateTimeOffset.Now,
                UserId = userId
            };

            _orderRepository.Insert(order);

            return Task.FromResult(order.Id);
        }

        public async Task<int> LeaseAsync(int orderId, int rentalId, DateTimeOffset start, DateTimeOffset end)
        {
            var oiEntity = _orderItemRepository
                .Table
                .FirstOrDefault(x => x.OrderId == orderId && x.RentalId == rentalId);

            if (oiEntity == null)
            {
                var oi = new OrderItemEntity
                {
                    OrderId = orderId,
                    RentalId = rentalId,
                    StartAt = start,
                    EndAt = end,
                    DailyPrice = 50,
                    Quantity = 1,
                    TaxRate = 0.25m
                };

                _orderItemRepository.Insert(oi);

                return await Task.FromResult(oi.Id);
            }

            oiEntity.StartAt = start;
            oiEntity.EndAt = end;

            _orderItemRepository.Update(oiEntity);

            var order = _orderRepository.GetById(orderId);
            order.UpdatedOn = DateTimeOffset.Now;

            var items = _orderItemRepository
                .Table
                .Where(x => x.OrderId == orderId);
            
            decimal subtotal = 0.0m;
            foreach (var itemEntity in items)
            {
                var minutes = (decimal)(itemEntity.EndAt - itemEntity.StartAt).TotalMinutes;

                subtotal += (itemEntity.DailyPrice * minutes) / 1400m;
            }

            order.OrderSubtotal = subtotal;
            order.OrderTotal = order.OrderSubtotal - (order.OrderSubtotal * order.OrderDiscount / 100);

            _orderRepository.Update(order);

            return await Task.FromResult(oiEntity.Id);
        }

        public async Task<int> ReleaseAsync(int orderId, int rentalId)
        {
            var oiEntity = _orderItemRepository
                .Table
                .FirstOrDefault(x => x.OrderId == orderId && x.RentalId == rentalId);

            if (oiEntity == null)
            {
                return 0;
            }

            var state = new VehicleStateEntity
            {
                IsTankFull = true,
                Odometer = 200200
            };

            oiEntity.State = state;

            if (!state.IsTankFull)
            {
                oiEntity.AdditionalCost = 100;
            }

            _orderItemRepository.Update(oiEntity);

            return await Task.FromResult(oiEntity.Id);
        }

        public Task<int> CompleteOrderAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            var order = await _orderRepository
                .Table
                .Where(x => x.Id == orderId)
                .FirstOrDefaultAsync();

            return _mapper.Map<Order>(order);
        }
    }
}
