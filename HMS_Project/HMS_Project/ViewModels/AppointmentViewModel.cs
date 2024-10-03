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
            ReceptionistName = Apointment.Receptionist.FullName;
            ClinicId = Apointment.ClinicId;
            ClinicName = Apointment.Clinic.Name;
            PatientId = Apointment.PatientId;
            PatientName = Apointment.Patient.FullName;
            DoctorId = Apointment.DoctorId;
            DoctorName = Apointment.Doctor.FullName;
            InvoiceId = Apointment.Invoice.InvoiceID;
            PrescriptionId = Apointment.Prescription.PrescriptionID;
        }

        [Required]
        public int ApointmentId { get; set; }

        [Required]
        public DateOnly ApointmentDate { get; set; }

        [Required]
        public TimeOnly ApointmentTime { get; set; }
        public string ApointmentStatus { get; set; } = null!;
        public virtual string Examination { get; set; } = null!;

        [Required]
        public long? ReceptionistId { get; set; }
        public string ReceptionistName { get; set; }

        [Required]
        public int? ClinicId { get; set; }
        public string ClinicName { get; set; }

        [Required]
        public long PatientId { get; set; }
        public string PatientName { get; set; }

        [Required]
        public long DoctorId { get; set; }
        public string DoctorName { get; set; }

        [Required]
        public int InvoiceId { get; set; }

        [Required]
        public int PrescriptionId { get; set; }

        public static explicit operator Apointment(ApointmentViewModel ApointmentViewModel)
        {
            return new Apointment()
            {
                ApointmentId = ApointmentViewModel.ApointmentId,
                ApointmentDate = ApointmentViewModel.ApointmentDate,
                ApointmentTime = ApointmentViewModel.ApointmentTime,
                ApointmentStatus = ApointmentViewModel.ApointmentStatus,
                //Examination = ApointmentViewModel.Examination,
                //ReceptionistId = ApointmentViewModel.ReceptionistId,
                //ClinicId = ApointmentViewModel.ClinicId,
                //PatientId = ApointmentViewModel.PatientId,
                //DoctorId = ApointmentViewModel.DoctorId,
            };
        }

        public static explicit operator ApointmentViewModel(Apointment Apointment)
        {
            return new ApointmentViewModel(Apointment);
        }
    }
}
