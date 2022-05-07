using System.Collections.Generic;
using Hire.Core.Domain.Customers;
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
            modelBuilder.ApplyConfiguration(new CustomerMap());
            modelBuilder.ApplyConfiguration(new OrderItemMap());
            modelBuilder.ApplyConfiguration(new OrderMap());
            modelBuilder.ApplyConfiguration(new RentalMap());
            modelBuilder.ApplyConfiguration(new VehicleMap());
            modelBuilder.ApplyConfiguration(new RentalStateMap());
            modelBuilder.ApplyConfiguration(new VehicleStateMap());

            modelBuilder.Entity<Customer>().HasData(new List<Customer>
            {
                new Customer
                {
                    Id = 1,
                    Name = "Florin CIOBANU",
                    Phone = "+34643447860"
                }
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
