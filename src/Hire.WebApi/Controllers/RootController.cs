using Hire.Services.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hire.WebApi.Controllers
{
    [Route("/")]
    [ApiController]
    [ApiVersion("1.0")]
    public class RootController : ControllerBase
    {
        [HttpGet(Name = nameof(GetRoot))]
        [ProducesResponseType(200)]
        public IActionResult GetRoot()
        {
            var response = new RootResponse
            {
                Self = Link.To(nameof(GetRoot)),
                Orders = Link.To(nameof(OrdersController.GetAllOrders)),
                Vehicles = Link.To(nameof(VehiclesController.GetAllVehicles))
            };

            return Ok(response);
        }
    }
}
