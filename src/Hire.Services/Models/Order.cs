using System;
using Hire.Services.Infrastructure;

namespace Hire.Services.Models
{
    public class Order : Resource
    {
        [Sortable]
        public DateTimeOffset Date { get; set; }

        public decimal Subtotal { get; set; }

        public decimal Discount { get; set; }

        public decimal OrderTotal { get; set; }
    }
}
