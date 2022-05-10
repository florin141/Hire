using System;
using Hire.Core.Domain.Rentals;
using Hire.Core.Domain.Returns;

namespace Hire.Core.Domain.Orders
{
    public class OrderItemEntity : BaseEntity
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
        public int? StateId { get; set; }

        /// <summary>
        /// Gets or sets the rental product start date
        /// </summary>
        public DateTimeOffset StartAt { get; set; }

        /// <summary>
        /// Gets or sets the rental product end date
        /// </summary>
        public DateTimeOffset EndAt { get; set; }

        /// <summary>
        /// Gets or sets the quantity
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the daily price in primary store currency
        /// </summary>
        public decimal DailyPrice { get; set; }

        /// <summary>
        /// Gets or sets any additional costs (i.e if tank not full)
        /// </summary>
        public decimal? AdditionalCost { get; set; }

        /// <summary>
        /// Gets or sets the tax rate
        /// </summary>
        public decimal TaxRate { get; set; }

        /// <summary>
        /// Gets or sets the status
        /// </summary>
        public OrderItemStatus ItemStatus { get; set; }

        /// <summary>
        /// Gets the order
        /// </summary>
        public virtual OrderEntity Order { get; set; }

        /// <summary>
        /// Gets the rental
        /// </summary>
        public virtual RentalEntity Rental { get; set; }

        /// <summary>
        /// Gets the rental
        /// </summary>
        public virtual RentalStateEntity State { get; set; }
    }
}
