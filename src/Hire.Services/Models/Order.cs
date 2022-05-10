using System;
using Hire.Services.Infrastructure;

namespace Hire.Services.Models
{
    public class Order : Resource
    {
        public decimal Subtotal { get; set; }

        public decimal Discount { get; set; }

        public decimal OrderTotal { get; set; }

        [Sortable]
        public DateTimeOffset Date { get; set; }

        public string Status { get; set; }
    }
}
