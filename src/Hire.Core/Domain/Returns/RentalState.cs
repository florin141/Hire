namespace Hire.Core.Domain.Returns
{
    public abstract class RentalState : BaseEntity
    {
        public decimal DamageCost { get; set; }
    }
}
