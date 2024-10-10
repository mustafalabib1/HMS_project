using DALProject.model;

namespace PLProject.ViewModels.AppointmentViewModel
{
    public class DayAvailability
    {
        public DateTime Date { get; set; }
        public bool IsAvailable { get; set; } = false;
        public List<DoctorScheduleLookup>? AvailableDoctors { get; set; }
    }
}
