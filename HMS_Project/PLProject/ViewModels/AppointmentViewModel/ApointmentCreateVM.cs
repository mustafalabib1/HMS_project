namespace PLProject.ViewModels.AppointmentViewModel
{
    public class ApointmentCreateVM
    {
        public int ClinicId { get; set; }
        public string SelectedDoctorUserId { get; set; }
        public string PatientUserId { get; set; }
        public string SelectedDate { get; set; }  // For the date
        public string SelectedTime { get; set; }  // For the time
    }
}
