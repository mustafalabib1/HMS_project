using DALProject.model;

namespace PLProject.ViewModels.AppointmentViewModel
{
    public class AppointmentGenarelVM
    {
        public int Id {  get; set; }
        public DateOnly ApointmentDate { get; set; }
        public TimeOnly? ApointmentTime { get; set; }
        public ApointmentStatusEnum ApointmentStatus { get; set; } 
        public virtual string? Examination { get; set; } = null!;

        public virtual Clinic Clinic { get; set; } = null!;

        public virtual Patient Patient { get; set; }= null!;

        public virtual Doctor Doctor { get; set; } = null!;

        public virtual Invoice Invoice { get; set; } = null!;
    }
}
