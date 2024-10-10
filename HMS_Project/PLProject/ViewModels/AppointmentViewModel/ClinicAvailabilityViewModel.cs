using DALProject.model;
using PLProject.Controllers;

namespace PLProject.ViewModels.AppointmentViewModel
{
    //Highlights the available days and clinics without specifically
    public class ClinicAvailabilityViewModel
    {
        public List<DayAvailability> AvailableDays { get; set; }
        public int? SelectedClinicId { get; set; } // Holds the selected clinic ID
        public int SelectedYear { get; set; }      // Holds the selected year
        public int SelectedMonth { get; set; }     // Holds the selected month
    }
}
