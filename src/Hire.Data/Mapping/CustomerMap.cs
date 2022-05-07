using Hire.Core.Domain.Customers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hire.Data.Mapping
{
    public class CustomerMap : HireEntityTypeConfiguration<Customer>
    {
        public override void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Name).HasMaxLength(256).IsRequired();

            builder.HasMany(x => x.Orders)
                .WithOne(x => x.Customer)
                .OnDelete(DeleteBehavior.Cascade);

            base.Configure(builder);
        }
    }
}
