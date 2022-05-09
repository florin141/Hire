using System.Threading.Tasks;
using Hire.Core.Domain.Rentals;
using Hire.Services.Models;

namespace Hire.Services.Rentals
{
    public interface IVehicleService
    {
        Task<bool> CheckAvailabilityAsync(int vehicleId);

        Task<PagedResults<Vehicle>> GetVehiclesAsync(
            PagingOptions pagingOptions, 
            SortOptions<Vehicle, VehicleEntity> sortOptions);

        Task<Vehicle> GetVehicleByIdAsync(int vehicleId);

        Task<VehicleEntity> GetVehicleEntityByIdAsync(int vehicleId);
    }
}
