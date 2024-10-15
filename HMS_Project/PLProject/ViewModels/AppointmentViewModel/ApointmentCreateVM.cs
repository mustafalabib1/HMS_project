namespace PLProject.ViewModels.AppointmentViewModel
{
    public class ApointmentCreateVM
    {
        public int ClinicId { get; set; }
        public string SelectedDoctorId { get; set; } = null!;
        public string PatientId { get; set; }= null!;
        public string SelectedDate { get; set; } = null!;  // For the date
        public string SelectedTime { get; set; } = null!;  // For the time
    }
}
