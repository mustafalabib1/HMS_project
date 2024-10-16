using DALProject.model;
using System;
using System.ComponentModel.DataAnnotations;

namespace Project.ViewModels
{
    public class DoctorScheduleVM
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Day")]
        public DayOfWeek Day { get; set; }

        [Required]
        [Display(Name = "Start Time")]
        public TimeSpan StartTime { get; set; } // Using TimeSpan instead of TimeOnly for broader compatibility in views

        [Required]
        [Display(Name = "End Time")]
        public TimeSpan EndTime { get; set; }

        [Required]
        public int DoctorId { get; set; }

        public Doctor? doctor { get; set; } 
        
    }
}
