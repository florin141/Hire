using System;
using System.Collections.Generic;
using System.Linq;
using Hire.Core.Data;
using Hire.Core.Domain.Customers;
using Hire.Core.Domain.Orders;
using Hire.Core.Domain.Rentals;
using Hire.Core.Domain.Returns;
using Hire.Core.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Hire.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly IRepository<Customer> _customerRepository;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<OrderItem> _orderItemRepository;
        private readonly IRepository<Vehicle> _vehicleRepository;

        public CustomerController(
            IRepository<Customer> customerRepository,
            IRepository<Order> orderRepository,
            IRepository<OrderItem> orderItemRepository,
            IRepository<Vehicle> vehicleRepository
            )
        {
            _customerRepository = customerRepository;
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
            _vehicleRepository = vehicleRepository;
        }

        [HttpGet]
        [Route("order")]
        public IEnumerable<Customer> GetCustomer()
        {
            var now = DateTimeOffset.Now;

            var car1 = new Vehicle
            {
                Vin = "JH4DC2380SS000012",
                Odometer = 100100,
                Make = "Chevrolet",
                Model = "Epica",
                Year = 2007,
                Type = VehicleType.Sedans
            };

            var car2 = new Vehicle
            {
                Vin = "JH4DC2380SS000012",
                Odometer = 200200,
                Make = "BMW",
                Model = "316i",
                Year = 2003,
                Type = VehicleType.Sedans,
            };

            var customer = new Customer
            {
                Name = "Florin CIOBANU",
                Phone = "+34643447860"
            };

            #region Register

            var order = new Order
            {
                Customer = customer
            };

            var stat1 = new VehicleState
            {
                DamageCost = 0,
                IsTankFull = true
            };

            order.OrderItems.Add(new OrderItem
            {
                StartDate = now.StartOfDay(),
                EndDate = now.EndOfDay(),
                DailyPrice = 25,
                Rental = car1,
                State = stat1
            });
            
            order.OrderItems.Add(new OrderItem
            {
                StartDate = now.StartOfDay(),
                EndDate = now.EndOfDay(),
                DailyPrice = 50,
                Rental = car2,
                State = stat1
            });

            _orderRepository.Insert(order);

            #endregion

            return _customerRepository.GetAll();
        }

        [HttpGet]
        [Route("return")]
        public IEnumerable<Order> GetOrder(string name)
        {
            var customer = _customerRepository
                .Table.FirstOrDefault(x => x.Name == "Florin CIOBANU");

            if (customer == null)
            {
                return Enumerable.Empty<Order>();
            }

            var orders = customer.Orders.ToList();

            return orders;
        }
    }
}
