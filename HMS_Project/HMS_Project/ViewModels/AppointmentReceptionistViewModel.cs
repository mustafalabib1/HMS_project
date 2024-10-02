using DALProject.model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS_Project.ViewModels
{
    internal class AppointmentReceptionistViewModel
    {
        public AppointmentReceptionistViewModel() { }

        // Constructor that takes an Appointment object
        public AppointmentReceptionistViewModel(Appointment appointment)
        {
            AppointmentId = appointment.AppointmentId;
            AppointmentDate = appointment.AppointmentDate;
            AppointmentTime = appointment.AppointmentTime;

            // Map enum to string to ensure correct status is displayed
            AppointmentStatus = Enum.GetName(typeof(AppointmentStatusEnum), appointment.AppointmentStatus) ?? AppointmentStatusEnum.Scheduled.ToString();
        }
        [Required]
        public int AppointmentId { get; set; }

        [Required]
        public DateOnly AppointmentDate { get; set; }

        [Required]
        public TimeOnly AppointmentTime { get; set; }
        public string AppointmentStatus { get; set; } = null!;

        public static explicit operator Appointment(AppointmentReceptionistViewModel appointmentViewModel)
        {
            return new Appointment()
            {
                AppointmentId   = appointmentViewModel.AppointmentId,
                AppointmentDate = appointmentViewModel.AppointmentDate,
                AppointmentTime = appointmentViewModel.AppointmentTime,
                AppointmentStatus = appointmentViewModel.AppointmentStatus,
            };
        }
        public static explicit operator AppointmentReceptionistViewModel(Appointment appointment)
        {
            return new AppointmentReceptionistViewModel(appointment);
        }
    }
}
