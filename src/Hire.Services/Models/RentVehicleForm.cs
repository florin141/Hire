using System;
using System.ComponentModel.DataAnnotations;

namespace Hire.Services.Models
{
    public class RentVehicleForm
    {
        [Required]
        [Display(Name = "startAt", Description = "Hire start time")]
        public DateTimeOffset? StartAt { get; set; }

        [Required]
        [Display(Name = "endAt", Description = "Hire end time")]
        public DateTimeOffset? EndAt { get; set; }
    }
}
