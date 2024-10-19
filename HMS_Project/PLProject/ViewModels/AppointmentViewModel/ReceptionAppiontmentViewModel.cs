using DALProject.model;
using PLProject.ViewModels.PrescriptionVM;

namespace PLProject.ViewModels.AppointmentViewModel
{
    public class ReceptionAppiontmentViewModel
    {
        public int Id { get; set; }
        public DateOnly ApointmentDate { get; set; }
        public TimeOnly? ApointmentTime { get; set; }
        public ApointmentStatusEnum ApointmentStatus { get; set; }
        public virtual Clinic? Clinic { get; set; } = null!;
        public virtual Patient? Patient { get; set; } = null!;
        public string? DoctorId { get; set; }
        public virtual Doctor? Doctor { get; set; } = null!;
        public virtual Invoice? Invoice { get; set; } = null!;
        public string? ReceptionistId { get; set; }
        public virtual Receptionist? Receptionist { get; set; } = null!;

    }
}
