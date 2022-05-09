namespace Hire.Core.Domain.Returns
{
    public class VehicleStateEntity : RentalStateEntity
    {
        public bool IsTankFull { get; set; }

        public int Odometer { get; set; }
    }
}
