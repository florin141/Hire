using System.Collections.Generic;
using Hire.Core.Domain.Rentals;
using Hire.Data.Mapping;
using Microsoft.EntityFrameworkCore;

namespace Hire.Data
{
    public sealed class HireObjectContext : DbContext
    {
        public HireObjectContext(DbContextOptions<HireObjectContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new OrderItemMap());
            modelBuilder.ApplyConfiguration(new OrderMap());
            modelBuilder.ApplyConfiguration(new RentalMap());
            modelBuilder.ApplyConfiguration(new RentalStateMap());

            modelBuilder.Entity<VehicleEntity>().HasData(new List<VehicleEntity>
            {
                new VehicleEntity
                {
                    Id = 1,
                    Price = 50,
                    Vin = "JH4DC2380SS000011",
                    Odometer = 100100,
                    Make = "Chevrolet",
                    Model = "Epica",
                    Year = 2007,
                    Type = VehicleType.Sedans
                },
                new VehicleEntity
                {
                    Id = 2,
                    Price = 80,
                    Vin = "JH4DC2380SS000012",
                    Odometer = 200200,
                    Make = "BMW",
                    Model = "316i",
                    Year = 2003,
                    Type = VehicleType.Sedans,
                },
                new VehicleEntity
                {
                    Id = 3,
                    Price = 120,
                    Vin = "TS4DC2380SS000013",
                    Odometer = 90100,
                    Make = "Honda",
                    Model = "Odyssey",
                    Year = 2022,
                    Type = VehicleType.Minivans,
                },
                new VehicleEntity
                {
                    Id = 4,
                    Price = 130,
                    Vin = "TS4DC2380SS000013",
                    Odometer = 90200,
                    Make = "Chrysler",
                    Model = "Pacifica",
                    Year = 2022,
                    Type = VehicleType.Minivans,
                },
                new VehicleEntity
                {
                    Id = 5,
                    Price = 130,
                    Vin = "JH4DC2380SS000014",
                    Odometer = 200100,
                    Make = "Chevrolet",
                    Model = "Colorado",
                    Year = 2022,
                    Type = VehicleType.Trucks,
                },
                new VehicleEntity
                {
                    Id = 6,
                    Price = 150,
                    Vin = "FD4DC2380SS000015",
                    Odometer = 200100,
                    Make = "Ford",
                    Model = "Maverick",
                    Year = 2022,
                    Type = VehicleType.Trucks,
                },
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
