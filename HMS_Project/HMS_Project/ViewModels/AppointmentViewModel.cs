using DALProject.model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS_Project.ViewModels
{
    public class ApointmentViewModel
    {
        public ApointmentViewModel() { }

        public ApointmentViewModel(Apointment Apointment)
        {
            ApointmentId = Apointment.ApointmentId;
            ApointmentDate = Apointment.ApointmentDate;
            ApointmentTime = Apointment.ApointmentTime;
            ApointmentStatus = ApointmentStatusEnum.Scheduled.ToString();
            Examination = Apointment.Examination;
            ReceptionistId = Apointment.ReceptionistId;
            ClinicId = Apointment.ClinicId;
            PatientId = Apointment.PatientId;
            DoctorId = Apointment.DoctorId;
            //InvoiceId = Apointment.Invoice.InvoiceID;
            //PrescriptionId = Apointment.Prescription.PrescriptionID;
        }

        [Required]
        public int ApointmentId { get; set; }

        [Display(Name = "Apointment Date")]
        [Required]
        public DateOnly ApointmentDate { get; set; }

        [Display(Name = "Apointment Time")]
        [Required]
        public TimeOnly ApointmentTime { get; set; }

        [Display(Name = "Apointment Status")]
        public string ApointmentStatus { get; set; } = null!;

        public virtual string Examination { get; set; } = null!;

        public IEnumerable<Clinic> Clinics { get; set; } = new HashSet<Clinic>();

        [Display(Name = "Clinic Name")]
        [Required(ErrorMessage = " Please Choose a Clinic for this Appointment.")]
        public int ClinicId { get; set; }

        public IEnumerable<Receptionist> Receptionists { get; set; } = new HashSet<Receptionist>();

        [Display(Name = "Receptionist Name")]
        [Required(ErrorMessage = " Please Choose a Receptionist for this Appointment.")]
        public long? ReceptionistId { get; set; }

        public IEnumerable<Doctor> Doctors { get; set; } = new HashSet<Doctor>();

        [Display(Name = "Doctor Name")]
        [Required(ErrorMessage = " Please Choose a Doctor for this Appointment.")]
        public long DoctorId { get; set; }

        // needs to be reviewed
        [Display(Name = "Patient Id")]
        [Required]
        public long PatientId { get; set; }
        // needs to be reviewed

        //[Required]
        //public int InvoiceId { get; set; }

        //[Required]
        //public int PrescriptionId { get; set; }

        public static explicit operator Apointment(ApointmentViewModel ApointmentViewModel)
        {
            return new Apointment()
            {
                ApointmentId = ApointmentViewModel.ApointmentId,
                ApointmentDate = ApointmentViewModel.ApointmentDate,
                ApointmentTime = ApointmentViewModel.ApointmentTime,
                ApointmentStatus = ApointmentViewModel.ApointmentStatus,
                Examination = ApointmentViewModel.Examination,
                ReceptionistId = ApointmentViewModel.ReceptionistId,
                ClinicId = ApointmentViewModel.ClinicId,
                PatientId = ApointmentViewModel.PatientId,
                DoctorId = ApointmentViewModel.DoctorId,
            };
        }

        public static explicit operator ApointmentViewModel(Apointment Apointment)
        {
            return new ApointmentViewModel(Apointment);
        }
    }
}
