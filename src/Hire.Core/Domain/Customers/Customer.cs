using System.Collections.Generic;
using Hire.Core.Domain.Orders;

namespace Hire.Core.Domain.Customers
{
    public class Customer : BaseEntity
    {
        private ICollection<Order> _orders;

        /// <summary>
        /// Customer name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Phone number
        /// </summary>
        public string Phone { get; set; }

        #region Navigation properties

        public virtual ICollection<Order> Orders
        {
            get { return _orders ?? (_orders = new HashSet<Order>()); }
            protected set { _orders = value; }
        }

        #endregion
    }
}
