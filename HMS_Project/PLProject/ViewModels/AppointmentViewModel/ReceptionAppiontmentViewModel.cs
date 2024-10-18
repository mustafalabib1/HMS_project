using DALProject.model;
using PLProject.ViewModels.PrescriptionVM;

namespace PLProject.ViewModels.AppointmentViewModel
{
    public class ReceptionAppiontmentViewModel
    {
        public ReceptionAppiontmentViewModel() { }
        public ReceptionAppiontmentViewModel(Apointment apointment) 
        {
            RecepId = apointment.Id;
            Patient = apointment.Patient;
            Doctor = apointment.Doctor;
            Clinic = apointment.Clinic;
            Invoice = apointment.Invoice;
        }
        public int RecepId { get; set; }
        public DateOnly ApointmentDate { get; set; }
        public TimeOnly? ApointmentTime { get; set; }
        public ApointmentStatusEnum ApointmentStatus { get; set; }
       
        public virtual Clinic? Clinic { get; set; } = null!;

        public virtual Patient? Patient { get; set; } = null!;
        public string? DoctorId { get; set; }
        public virtual Doctor? Doctor { get; set; } = null!;

        public virtual Invoice? Invoice { get; set; } = null!;

        public static explicit operator Apointment(ReceptionAppiontmentViewModel receptionAppiontmentView)
        {
            return new Apointment
            {
                Id = receptionAppiontmentView.RecepId,
                Patient = receptionAppiontmentView.Patient,
                Doctor = receptionAppiontmentView.Doctor,
                Clinic = receptionAppiontmentView.Clinic,
                Invoice = receptionAppiontmentView.Invoice

            };
        }
        public static explicit operator ReceptionAppiontmentViewModel(Apointment apointment)
        {
            return new ReceptionAppiontmentViewModel(apointment);
        }
    }
}
