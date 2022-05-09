using System.ComponentModel.DataAnnotations;

namespace Hire.Services.Models
{
    public class ReleaseVehicleForm
    {
        [Required]
        [Display(Name = "isTankFull", Description = "Is tank full")]
        public bool IsTankFull { get; set; }

        [Required]
        [Display(Name = "odometer", Description = "Odometer")]
        public int Odometer { get; set; }
    }
}
