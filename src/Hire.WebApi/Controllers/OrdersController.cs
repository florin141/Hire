using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hire.Core.Extensions;
using Hire.Services.Models;
using Hire.Services.Orders;
using Hire.Services.Rentals;
using Microsoft.AspNetCore.Mvc;

namespace Hire.WebApi.Controllers
{
    /// <summary>
    /// CRUD 
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [ApiVersion("1.0")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IVehicleService _vehicleService;

        public OrdersController(
            IOrderService orderService, IVehicleService vehicleService)
        {
            _orderService = orderService;
            _vehicleService = vehicleService;
        }

        [HttpGet(Name = nameof(GetAllOrders))]
        [ProducesResponseType(200)]
        public async Task<ActionResult<Collection<Order>>> GetAllOrders()
        {
            var userId = 1;

            var orders = await _orderService.GetOrdersAsync(userId);
            return Ok(orders);
        }

        [HttpGet(Name = nameof(GetOrderById))]
        [ProducesResponseType(200)]
        [Route("{orderId}")]
        public async Task<ActionResult> GetOrderById([FromRoute] int orderId)
        {
            var order = await _orderService.GetOrderByIdAsync(orderId);
            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        [HttpPost("{userId}/init", Name = nameof(InitOrder))]
        [ProducesResponseType(200)]
        [ProducesResponseType(201)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> InitOrder(int userId)
        {
            var orderId = await _orderService.BeginOrderAsync(userId);

            return Created(Url.Link(nameof(OrdersController.GetOrderById), new { orderId }), null);
        }

        [HttpPost("{orderId}/{rentalId}/lease", Name = nameof(RentVehicle))]
        [ProducesResponseType(200)]
        [ProducesResponseType(201)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> RentVehicle(int orderId,int rentalId, [FromBody] RentVehicleForm rentForm)
        {
            var order = await _orderService.GetOrderByIdAsync(orderId);
            if (order == null)
            {
                return NotFound();
            }

            var vehicle = await _vehicleService.GetVehicleEntityByIdAsync(rentalId);
            if (vehicle == null)
            {
                return NotFound();
            }

            var now = DateTimeOffset.Now;

            await _orderService.LeaseAsync(
                orderId, 
                rentalId, 
                rentForm.StartAt.GetValueOrDefault(now.StartOfDay()),
                rentForm.EndAt.GetValueOrDefault(now.EndOfDay()));

            return Created(Url.Link(nameof(OrdersController.GetOrderById), new { orderId }), null);
        }

        [HttpPost("{orderId}/{rentalId}/release", Name = nameof(ReturnVehicle))]
        [ProducesResponseType(200)]
        [ProducesResponseType(201)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> ReturnVehicle(int orderId, int rentalId, [FromBody] ReleaseVehicleForm releaseForm)
        {
            var order = await _orderService.GetOrderByIdAsync(orderId);
            if (order == null)
            {
                return NotFound();
            }

            var vehicle = await _vehicleService.GetVehicleEntityByIdAsync(rentalId);
            if (vehicle == null)
            {
                return NotFound();
            }
            
            await _orderService.ReleaseAsync(orderId, rentalId);

            return Created(Url.Link(nameof(OrdersController.GetOrderById), new { orderId }), null);
        }
    }
}
