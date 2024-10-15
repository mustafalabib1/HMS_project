using DALProject.model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLProject.ViewModels
{
    public class ApointmentViewModel
    {
        public ApointmentViewModel() { }

        public ApointmentViewModel(Apointment Apointment)
        {
            ApointmentId = Apointment.Id;
            ApointmentDate = Apointment.ApointmentDate;
            ApointmentTime = Apointment.ApointmentTime;
            ApointmentStatus = ApointmentStatusEnum.Scheduled;
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
        public TimeOnly? ApointmentTime { get; set; }

        [Display(Name = "Apointment Status")]
        public ApointmentStatusEnum ApointmentStatus { get; set; } 
        public virtual string Examination { get; set; } = null!;

        public IEnumerable<Clinic> Clinics { get; set; } = new HashSet<Clinic>();

        [Display(Name = "Clinic Name")]
        [Required(ErrorMessage = " Please Choose a Clinic for this Appointment.")]
        public int ClinicId { get; set; }

        public IEnumerable<Receptionist> Receptionists { get; set; } = new HashSet<Receptionist>();

        [Display(Name = "Receptionist Name")]
        [Required(ErrorMessage = " Please Choose a Receptionist for this Appointment.")]
        public string? ReceptionistId { get; set; }

        public IEnumerable<Doctor> Doctors { get; set; } = new HashSet<Doctor>();

        [Display(Name = "Doctor Name")]
        [Required(ErrorMessage = " Please Choose a Doctor for this Appointment.")]
        public string DoctorId { get; set; }

        // needs to be reviewed
        [Display(Name = "Patient Id")]
        [Required]
        public string PatientId { get; set; }
        // needs to be reviewed

        //[Required]
        //public int InvoiceId { get; set; }

        //[Required]
        //public int PrescriptionId { get; set; }

        public static explicit operator Apointment(ApointmentViewModel ApointmentViewModel)
        {
            return new Apointment()
            {
                Id = ApointmentViewModel.ApointmentId,
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
