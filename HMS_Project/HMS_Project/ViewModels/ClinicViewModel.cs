using DALProject.model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS_Project.ViewModels
{
    internal class ClinicViewModel
    {
        public ClinicViewModel() { }
        public ClinicViewModel(Clinic clinic)
        {
            //ClinicId = clinic.ClinicId;
            Name = clinic.Name;
            Phone = clinic.Phone;
            Price = clinic.Price;
            Specilization = clinic.Specilization;
            DoctorsInClinic = (HashSet<Doctor>)clinic.Doctors;
            //UpcomingAppointments = (HashSet<Appointment>)clinic.Appointments;

            //TotalDoctors = clinic.Doctors.Count;
            //TotalNurses = clinic.Nurses.Count;
            //TotalAppointments = clinic.Appointments.Count;
        }

        //[Required]
        //public int ClinicId { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public string Phone { get; set; } = null!;
        public ICollection<ClinicSpecializationLookup> SpecializationsDateReader { get; set; } = new HashSet<ClinicSpecializationLookup>();
        [Required]
        public string Specilization { get; set; } = null!;
        [Required]
        public double Price { get; set; }
        public IEnumerable<Nurse> NursesDateReader { get; set; } = new HashSet<Nurse>();
        public ICollection<Nurse> NursesInClinic { get; set; } = new HashSet<Nurse>();
        public IEnumerable<Doctor> DoctorsDateReader { get; set; } = new HashSet<Doctor>();
        public ICollection<Doctor> DoctorsInClinic { get; set; } = new HashSet<Doctor>();

        //HashSet<Appointment> UpcomingAppointments { get; set; } = new HashSet<Appointment>();

        //// Maybe if we want to display the number of Doctors, Nurses and Appointments in the clinic we should use it new view 
        //public int TotalDoctors { get; set; }
        //public int TotalNurses { get; set; }
        //public int TotalAppointments { get; set; }


        public static explicit operator Clinic(ClinicViewModel _clinicViewModel)
        {
            return new Clinic()
            {
                //ClinicId = _clinicViewModel.ClinicId,
                Name = _clinicViewModel.Name,
                Phone = _clinicViewModel.Phone,
                Price = _clinicViewModel.Price,
                Specilization = _clinicViewModel.Specilization,
                Doctors = _clinicViewModel.DoctorsInClinic,
                //Appointments = _clinicViewModel.UpcomingAppointments
            };
        }

        public static explicit operator ClinicViewModel(Clinic _clinic)
        {
            return new ClinicViewModel(_clinic);
        }
    }
}
