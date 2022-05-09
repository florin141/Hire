using Hire.Services.Infrastructure;

namespace Hire.Services.Models
{
    public class Vehicle : Resource
    {
        public string Vin { get; set; }

        [Sortable(Default = true)]
        public int Odometer { get; set; }

        [Sortable]
        public string Make { get; set; }

        public string Model { get; set; }

        [Sortable]
        public int Year { get; set; }

        public string Type { get; set; }
    }
}
