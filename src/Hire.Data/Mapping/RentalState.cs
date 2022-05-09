using Hire.Core.Domain.Rentals;
using Hire.Core.Domain.Returns;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hire.Data.Mapping
{
    public class RentalStateMap : HireEntityTypeConfiguration<RentalStateEntity>
    {
        public override void Configure(EntityTypeBuilder<RentalStateEntity> builder)
        {
            builder.ToTable("RentalState");

            builder.HasKey(a => a.Id);

            builder
                .HasDiscriminator<RentalType>("RentalType")
                .HasValue(typeof(VehicleStateEntity), RentalType.Vehicle);

            builder.HasIndex("RentalType");

            base.Configure(builder);
        }
    }
}
