using System.Collections.Generic;
using Hire.Core.Domain.Customers;

namespace Hire.Core.Domain.Orders
{
    public class Order : BaseEntity
    {
        private ICollection<OrderItem> _orderItems;

        public int CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the order subtotal
        /// </summary>
        public decimal OrderSubtotal { get; set; }

        #region Navigation properties

        public virtual Customer Customer { get; set; }

        /// <summary>
        /// Gets or sets order items
        /// </summary>
        public virtual ICollection<OrderItem> OrderItems
        {
            get { return _orderItems ?? (_orderItems = new HashSet<OrderItem>()); }
            protected set { _orderItems = value; }
        }

        #endregion
    }
}
