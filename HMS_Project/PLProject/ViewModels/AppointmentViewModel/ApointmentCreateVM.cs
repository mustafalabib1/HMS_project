using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace PLProject.ViewModels.AppointmentViewModel
{
    public class ApointmentCreateVM
    {
        public int ClinicId { get; set; }
        public string SelectedDoctorId { get; set; }
        [ValidateNever]
        public string PatientId { get; set; }
        public string SelectedDate { get; set; }  // For the date
        public string SelectedTime { get; set; }  // For the time
    }
}
