using System;
using Hire.Core.Domain.Rentals;
using Hire.Core.Domain.Returns;

namespace Hire.Core.Domain.Orders
{
    public class OrderItem : BaseEntity
    {
        /// <summary>
        /// Gets or sets the order identifier
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// Gets or sets the rental identifier
        /// </summary>
        public int RentalId { get; set; }

        /// <summary>
        /// Gets or sets the rental state identifier
        /// </summary>
        public int StateId { get; set; }

        /// <summary>
        /// Gets or sets the start date of rental
        /// </summary>
        public DateTimeOffset StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date of rental
        /// </summary>
        public DateTimeOffset EndDate { get; set; }

        /// <summary>
        /// Gets or sets the daily price in primary store currency
        /// </summary>
        public decimal DailyPrice { get; set; }

        /// <summary>
        /// Gets the order
        /// </summary>
        public virtual Order Order { get; set; }

        /// <summary>
        /// Gets the rental
        /// </summary>
        public virtual Rental Rental { get; set; }

        /// <summary>
        /// Gets the rental
        /// </summary>
        public virtual RentalState State { get; set; }
    }
}
