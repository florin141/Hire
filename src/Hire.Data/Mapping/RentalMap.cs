using Hire.Core.Domain.Rentals;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hire.Data.Mapping
{
    public class RentalMap : HireEntityTypeConfiguration<Rental>
    {
        public override void Configure(EntityTypeBuilder<Rental> builder)
        {
            builder.ToTable(nameof(Rental));

            builder.HasKey(a => a.Id);

            base.Configure(builder);
        }
    }
}
