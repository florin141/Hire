using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hire.Core.Data;
using Hire.Core.Domain.Rentals;
using Hire.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace Hire.Services.Rentals
{
    public class VehicleService : IVehicleService
    {
        private readonly IRepository<VehicleEntity> _vehicleRepository;
        private readonly IConfigurationProvider _mappingConfiguration;

        public VehicleService(
            IRepository<VehicleEntity> vehicleRepository, 
            IConfigurationProvider mappingConfiguration)
        {
            _vehicleRepository = vehicleRepository;
            _mappingConfiguration = mappingConfiguration;
        }

        public Task<bool> CheckAvailabilityAsync(int vehicleId)
        {
            return Task.FromResult(true);
        }

        public async Task<PagedResults<Vehicle>> GetVehiclesAsync(
            PagingOptions pagingOptions, 
            SortOptions<Vehicle, VehicleEntity> sortOptions)
        {
            var query = _vehicleRepository.Table;

            query = sortOptions.Apply(query);

            var size = await query.CountAsync();

            var items = await query
                .Skip(pagingOptions.Offset.Value)
                .Take(pagingOptions.Limit.Value)
                .ProjectTo<Vehicle>(_mappingConfiguration)
                .ToArrayAsync();

            return new PagedResults<Vehicle>
            {
                Items = items,
                TotalSize = size
            };
        }

        public async Task<VehicleEntity> GetVehicleEntityByIdAsync(int id)
        {
            var entity = _vehicleRepository.GetById(id);
            if (entity == null)
            {
                return null;
            }

            return entity;
        }

        public async Task<Vehicle> GetVehicleByIdAsync(int vehicleId)
        {
            var entity = await GetVehicleEntityByIdAsync(vehicleId);

            var mapper = _mappingConfiguration.CreateMapper();

            return mapper.Map<Vehicle>(entity);
        }
    }
}
