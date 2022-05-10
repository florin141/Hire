using System;
using System.Collections.Generic;

namespace Hire.Core.Domain.Orders
{
    public class OrderEntity : BaseEntity, IAuditable
    {
        private ICollection<OrderItemEntity> _orderItems;

        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the order subtotal
        /// </summary>
        public decimal Subtotal { get; set; }

        /// <summary>
        /// Gets or sets the order discount (applied to order total)
        /// </summary>
        public decimal Discount { get; set; }

        /// <summary>
        /// Gets or sets the order total
        /// </summary>
        public decimal Total { get; set; }

        public OrderStatus Status { get; set; }

        public DateTimeOffset CreatedOn { get; set; }

        public DateTimeOffset? UpdatedOn { get; set; }

        #region Navigation properties

        /// <summary>
        /// Gets or sets order items
        /// </summary>
        public virtual ICollection<OrderItemEntity> OrderItems
        {
            get { return _orderItems ?? (_orderItems = new HashSet<OrderItemEntity>()); }
            protected set { _orderItems = value; }
        }

        #endregion
    }
}
