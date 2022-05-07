using Hire.Core.Domain.Returns;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hire.Data.Mapping
{
    class VehicleStateMap : HireEntityTypeConfiguration<VehicleState>
    {
        public override void Configure(EntityTypeBuilder<VehicleState> builder)
        {
            builder.ToTable(nameof(VehicleState));

            base.Configure(builder);
        }
    }
}
