using System.Linq;
using System.Threading.Tasks;
using Hire.Core.Domain.Rentals;
using Hire.Services.Models;
using Hire.Services.Orders;
using Hire.Services.Rentals;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Hire.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ApiVersion("1.0")]
    public class VehiclesController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;
        private readonly PagingOptions _defaultPagingOptions;

        public VehiclesController(
            IVehicleService vehicleService,
            IOrderService orderService,
            IOptions<PagingOptions> pagingOptions)
        {
            _vehicleService = vehicleService;
            _defaultPagingOptions = pagingOptions.Value;
        }

        [HttpGet(Name = nameof(GetAllVehicles))]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Collection<Vehicle>>> GetAllVehicles(
            [FromQuery] PagingOptions pagingOptions,
            [FromQuery] SortOptions<Vehicle, VehicleEntity> sortOptions)
        {
            var ownerId = User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;

            pagingOptions.Offset = pagingOptions.Offset ?? _defaultPagingOptions.Offset;
            pagingOptions.Limit = pagingOptions.Limit ?? _defaultPagingOptions.Limit;

            var vehicles = await _vehicleService.GetVehiclesAsync(pagingOptions, sortOptions);

            var collection = PagedCollection<Vehicle>.Create(
                Link.ToCollection(nameof(GetAllVehicles)),
                vehicles.Items.ToArray(),
                vehicles.TotalSize,
                pagingOptions);

            return Ok(collection);
        }

        [Route("{vehicleId}", Name = nameof(GetVehicleById))]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Vehicle>> GetVehicleById(int vehicleId)
        {
            var vehicle = await _vehicleService.GetVehicleByIdAsync(vehicleId);
            if (vehicle == null)
            {
                return NotFound();
            }

            return vehicle;
        }
    }
}
