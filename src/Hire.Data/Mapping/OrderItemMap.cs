using Hire.Core.Domain.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hire.Data.Mapping
{
    public class OrderItemMap : HireEntityTypeConfiguration<OrderItemEntity>
    {
        public override void Configure(EntityTypeBuilder<OrderItemEntity> builder)
        {
            builder.ToTable("OrderItem");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.DailyPrice).IsRequired();

            builder.HasOne(x => x.Order);
            builder.HasOne(x => x.Rental);
            builder.HasOne(x => x.State);

            base.Configure(builder);
        }
    }
}
