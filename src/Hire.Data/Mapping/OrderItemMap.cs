using Hire.Core.Domain.Orders;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hire.Data.Mapping
{
    public class OrderItemMap : HireEntityTypeConfiguration<OrderItem>
    {
        public override void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.DailyPrice).IsRequired();
            builder.Property(o => o.DailyPrice).HasPrecision(18, 8);

            builder.HasOne(x => x.Order);
            builder.HasOne(x => x.Rental);
            builder.HasOne(x => x.State);

            base.Configure(builder);
        }
    }
}
