using Hire.Core.Domain.Rentals;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hire.Data.Mapping
{
    public class RentalMap : HireEntityTypeConfiguration<RentalEntity>
    {
        public override void Configure(EntityTypeBuilder<RentalEntity> builder)
        {
            builder.ToTable("Rental");

            builder.HasKey(a => a.Id);

            builder
                .HasDiscriminator<RentalType>("RentalType")
                .HasValue(typeof(VehicleEntity), RentalType.Vehicle);

            builder.HasIndex("RentalType");

            base.Configure(builder);
        }
    }
}
