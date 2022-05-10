using System.Linq;
using System.Threading.Tasks;
using Hire.Services.Models;
using Hire.Services.Orders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Hire.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ApiVersion("1.0")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly PagingOptions _defaultPagingOptions;

        public OrdersController(
            IOrderService orderService,
            IOptions<PagingOptions> pagingOptions)
        {
            _orderService = orderService;
            _defaultPagingOptions = pagingOptions.Value;
        }

        [HttpGet(Name = nameof(GetAllOrders))]
        [ProducesResponseType(200)]
        public async Task<ActionResult<Collection<Order>>> GetAllOrders([FromQuery] PagingOptions pagingOptions)
        {
            pagingOptions.Offset = pagingOptions.Offset ?? _defaultPagingOptions.Offset;
            pagingOptions.Limit = pagingOptions.Limit ?? _defaultPagingOptions.Limit;

            var orders = await _orderService.GetAllOrdersAsync(pagingOptions);

            var collection = PagedCollection<Order>.Create(
                Link.ToCollection(nameof(GetAllOrdersByUserId)),
                orders.Items.ToArray(),
                orders.TotalSize,
                pagingOptions);

            return Ok(collection);
        }

        [Route("{userId}/all", Name = nameof(GetAllOrdersByUserId))]
        [ProducesResponseType(200)]
        public async Task<ActionResult<Order>> GetAllOrdersByUserId(
            int userId, 
            [FromQuery] PagingOptions pagingOptions)
        {
            pagingOptions.Offset = pagingOptions.Offset ?? _defaultPagingOptions.Offset;
            pagingOptions.Limit = pagingOptions.Limit ?? _defaultPagingOptions.Limit;

            var orders = await _orderService.GetOrdersAsync(userId, pagingOptions);

            var collection = PagedCollection<Order>.Create(
                Link.ToCollection(nameof(GetAllOrdersByUserId)),
                orders.Items.ToArray(),
                orders.TotalSize,
                pagingOptions);

            return Ok(collection);
        }

        [Route("{orderId}", Name = nameof(GetOrderById))]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Order>> GetOrderById(int orderId)
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

        [HttpPost("{orderId}/complete", Name = nameof(CompleteOrder))]
        [ProducesResponseType(200)]
        [ProducesResponseType(201)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> CompleteOrder(int orderId)
        {
            var order = await _orderService.GetOrderByIdAsync(orderId);
            if (order == null)
            {
                return NotFound();
            }
            if (order.Status == "COMPLETE" || order.Status == "CANCELLED")
            {
                return NoContent();
            }

            await _orderService.CompleteOrderAsync(orderId);

            return Created(Url.Link(nameof(OrdersController.GetOrderById), new { orderId }), null);
        }
    }
}
