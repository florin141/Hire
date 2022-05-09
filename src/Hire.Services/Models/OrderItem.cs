using System;

namespace Hire.Services.Models
{
    public class OrderItem : Resource
    {
        public int OrderId { get; set; }

        public int RentalId { get; set; }

        public DateTimeOffset StartAt { get; set; }

        public DateTimeOffset EndAt { get; set; }
        
        public decimal DailyPrice { get; set; }
        
        public decimal? AdditionalCost { get; set; }
    }
}
