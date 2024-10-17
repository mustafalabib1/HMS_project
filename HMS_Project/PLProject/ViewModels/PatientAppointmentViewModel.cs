using DALProject.model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLProject.ViewModels
{
    public class BookingApointmentViewModel
    {

        public BookingApointmentViewModel()
        {
        }
        public BookingApointmentViewModel(Apointment Apointment)
        {
            ApointmentDate=Apointment.ApointmentDate;
            ApointmentStatus =Apointment.ApointmentStatus;
            ClinicId=Apointment.ClinicId;
            DoctorUserId = Apointment.DoctorUserId;
        }
        [Required]
        public DateOnly ApointmentDate { get; set; }
        public ApointmentStatusEnum ApointmentStatus { get; set; } = ApointmentStatusEnum.Scheduled;

        public IEnumerable<Clinic> ClinicsDateReader { get; set; }= new HashSet<Clinic>();
        [Required]
        public int ClinicId { get; set; }
        
        public IEnumerable<Doctor> DoctorsDateReader { get;set; }= new HashSet<Doctor>();
        [Required]
        public string DoctorUserId{ get; set; }

        public static explicit operator Apointment(BookingApointmentViewModel ViewModel)
        {
            return new Apointment()
            {
                ApointmentDate = ViewModel.ApointmentDate,
                ApointmentStatus = ViewModel.ApointmentStatus,
                ClinicId = ViewModel.ClinicId,
                DoctorUserId = ViewModel.DoctorUserId,
            };
        }
        public static explicit operator BookingApointmentViewModel(Apointment Apointment)
        {
            return new BookingApointmentViewModel(Apointment);
        }
    }
}
