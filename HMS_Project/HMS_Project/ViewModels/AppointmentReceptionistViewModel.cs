using DALProject.model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS_Project.ViewModels
{
    internal class ApointmentReceptionistViewModel
    {
        public ApointmentReceptionistViewModel() { }

        // Constructor that takes an Apointment object
        public ApointmentReceptionistViewModel(Apointment Apointment)
        {
            Id = Apointment.Id;
            ApointmentDate = Apointment.ApointmentDate;
            ApointmentTime = Apointment.ApointmentTime;

            // Map enum to string to ensure correct status is displayed
            ApointmentStatus = Enum.GetName(typeof(ApointmentStatusEnum), Apointment.ApointmentStatus) ?? ApointmentStatusEnum.Scheduled.ToString();
        }
        [Required]
        public int Id { get; set; }

        [Required]
        public DateOnly ApointmentDate { get; set; }

        [Required]
        public TimeOnly ApointmentTime { get; set; }
        public string ApointmentStatus { get; set; } = null!;

        public static explicit operator Apointment(ApointmentReceptionistViewModel ApointmentViewModel)
        {
            return new Apointment()
            {
                Id   = ApointmentViewModel.Id,
                ApointmentDate = ApointmentViewModel.ApointmentDate,
                ApointmentTime = ApointmentViewModel.ApointmentTime,
                ApointmentStatus = ApointmentViewModel.ApointmentStatus,
            };
        }
        public static explicit operator ApointmentReceptionistViewModel(Apointment Apointment)
        {
            return new ApointmentReceptionistViewModel(Apointment);
        }
    }
}
