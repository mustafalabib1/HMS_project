using DALProject.model;
using PLProject.ViewModels.PrescriptionVM;

namespace PLProject.ViewModels.AppointmentViewModel
{
    public class AppointmentGenarelVM
    {
        public int Id {  get; set; }
        public DateOnly ApointmentDate { get; set; }
        public TimeOnly? ApointmentTime { get; set; }
        public ApointmentStatusEnum ApointmentStatus { get; set; } 
        public  string? Examination { get; set; } = null!;

        public virtual Clinic? Clinic { get; set; } = null!;

        public virtual Patient? Patient { get; set; }= null!;
        public  string? DoctorId { get; set; }
        public virtual Doctor? Doctor { get; set; } = null!;

        public virtual Invoice? Invoice { get; set; } = null!;
        public int? PrescriptionId { get; set; }
        public virtual PrescriptionViewModel? PrescriptionViewModel { get; set; }
    }
}
