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
            ClinicId = clinic.ClinicId;
            Name = clinic.Name;
            Phone = clinic.Phone;
            Price = clinic.Price;
            Specilization = clinic.Specilization;
            Doctors = (HashSet<Doctor>)clinic.Doctors;
            UpcomingApointments = (HashSet<Apointment>)clinic.Apointments;

            TotalDoctors = clinic.Doctors.Count;
            TotalNurses = clinic.Nurses.Count;
            TotalAppointments = clinic.Apointments.Count;
        }

        [Required]
        public int ClinicId { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public string Phone { get; set; } = null!;
        [Required]
        public string Specilization { get; set; } = null!;
        [Required]
        public double Price { get; set; }
        HashSet<Doctor> Doctors { get; set; } = new HashSet<Doctor>();

        HashSet<Apointment> UpcomingApointments { get; set; } = new HashSet<Apointment>();

        // Maybe if we want to display the number of Doctors, Nurses and Appointments in the clinic
        public int TotalDoctors { get; set; }
        public int TotalNurses { get; set; }
        public int TotalAppointments { get; set; }


        public static explicit operator Clinic(ClinicViewModel _clinicViewModel)
        {
            return new Clinic()
            {
                ClinicId = _clinicViewModel.ClinicId,
                Name = _clinicViewModel.Name,
                Phone = _clinicViewModel.Phone,
                Price = _clinicViewModel.Price,
                Specilization = _clinicViewModel.Specilization,
                Doctors = _clinicViewModel.Doctors,
                Apointments = _clinicViewModel.UpcomingApointments
            };
        }

        public static explicit operator ClinicViewModel(Clinic _clinic)
        {
            return new ClinicViewModel(_clinic);
        }
    }
}
