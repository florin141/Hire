using Hire.Core.Domain.Returns;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hire.Data.Mapping
{
    public class RentalStateMap : HireEntityTypeConfiguration<RentalState>
    {
        public override void Configure(EntityTypeBuilder<RentalState> builder)
        {
            builder.ToTable(nameof(RentalState));

            builder.HasKey(a => a.Id);

            base.Configure(builder);
        }
    }
}
