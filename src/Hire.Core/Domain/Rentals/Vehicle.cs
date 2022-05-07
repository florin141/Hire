namespace Hire.Core.Domain.Rentals
{
    public class Vehicle : Rental
    {
        /// <summary>
        /// Vehicle Identification Number (VIN)
        /// </summary>
        public string Vin { get; set; }

        public int Odometer { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public int Year { get; set; }

        public VehicleType Type { get; set; }
    }
}
