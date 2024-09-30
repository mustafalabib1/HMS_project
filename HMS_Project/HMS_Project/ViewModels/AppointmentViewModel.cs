using DALProject.model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS_Project.ViewModels
{
    internal class AppointmentViewModel
    {
        public AppointmentViewModel() { }

        public AppointmentViewModel(Appointment appointment)
        {
            AppointmentId = appointment.AppointmentId;
            AppointmentDate = appointment.AppointmentDate;
            AppointmentTime = appointment.AppointmentTime;
            AppointmentStatus = AppointmentStatusEnum.Scheduled.ToString();
            Examination = appointment.Examination;
            ReceptionistId = appointment.ReceptionistId;
            ReceptionistName = appointment.Receptionist.FullName;
            ClinicId = appointment.ClinicId;
            ClinicName = appointment.Clinic.Name;
            PatientId = appointment.PatientId;
            PatientName = appointment.Patient.FullName;
            DoctorId = appointment.DoctorId;
            DoctorName = appointment.Doctor.FullName;
            InvoiceId = appointment.Invoice.InvoiceID;
            PrescriptionId = appointment.Prescription.PrescriptionID;
        }

        [Required]
        public int AppointmentId { get; set; }

        [Required]
        public DateOnly AppointmentDate { get; set; }

        [Required]
        public TimeOnly AppointmentTime { get; set; }
        public string AppointmentStatus { get; set; } = null!;
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

        public static explicit operator Appointment(AppointmentViewModel appointmentViewModel)
        {
            return new Appointment()
            {
                AppointmentId = appointmentViewModel.AppointmentId,
                AppointmentDate = appointmentViewModel.AppointmentDate,
                AppointmentTime = appointmentViewModel.AppointmentTime,
                AppointmentStatus = appointmentViewModel.AppointmentStatus,
                //Examination = appointmentViewModel.Examination,
                //ReceptionistId = appointmentViewModel.ReceptionistId,
                //ClinicId = appointmentViewModel.ClinicId,
                //PatientId = appointmentViewModel.PatientId,
                //DoctorId = appointmentViewModel.DoctorId,
            };
        }

        public static explicit operator AppointmentViewModel(Appointment appointment)
        {
            return new AppointmentViewModel(appointment);
        }
    }
}
