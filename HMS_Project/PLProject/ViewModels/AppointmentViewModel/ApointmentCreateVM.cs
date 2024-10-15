namespace PLProject.ViewModels.AppointmentViewModel
{
    public class ApointmentCreateVM
    {
        public int ClinicId { get; set; }
        public int SelectedDoctorId { get; set; }
        public int PatientId { get; set; }
        public string SelectedDate { get; set; }  // For the date
        public string SelectedTime { get; set; }  // For the time
    }
}
