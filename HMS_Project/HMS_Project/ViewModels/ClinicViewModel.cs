using DALProject.model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS_Project.ViewModels
{
    public class ClinicViewModel
    {
        public ClinicViewModel() { }
        public ClinicViewModel(Clinic clinic)
        {
            //ClinicId = clinic.ClinicId;
            Name = clinic.Name;
            Phone = clinic.Phone;
            Price = clinic.Price;
            SpecilizationID = clinic.ClinicSpecializationId;
            DoctorsInClinic = (HashSet<Doctor>)clinic.Doctors;
            //UpcomingApointments = (HashSet<Apointment>)clinic.Apointments;

            //TotalDoctors = clinic.Doctors.Count;
            //TotalNurses = clinic.Nurses.Count;
            //TotalApointments = clinic.Apointments.Count;
        }
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public string Phone { get; set; } = null!;
        public IEnumerable<ClinicSpecializationLookup> SpecializationsDateReader { get; set; } = new HashSet<ClinicSpecializationLookup>();
        [Required]
        public int? SpecilizationID { get; set; } 
        [Required]
        public double Price { get; set; }
        public IEnumerable<Nurse> NursesDateReader { get; set; } = new HashSet<Nurse>();
        public ICollection<Nurse> NursesInClinic { get; set; } = new HashSet<Nurse>();
        public IEnumerable<Doctor> DoctorsDateReader { get; set; } = new HashSet<Doctor>();
        public ICollection<Doctor> DoctorsInClinic { get; set; } = new HashSet<Doctor>();

        //HashSet<Apointment> UpcomingApointments { get; set; } = new HashSet<Apointment>();

        //// Maybe if we want to display the number of Doctors, Nurses and Apointments in the clinic we should use it new view 
        //public int TotalDoctors { get; set; }
        //public int TotalNurses { get; set; }
        //public int TotalApointments { get; set; }


        public static explicit operator Clinic(ClinicViewModel _clinicViewModel)
        {
            return new Clinic()
            {
                //ClinicId = _clinicViewModel.ClinicId,
                Name = _clinicViewModel.Name,
                Phone = _clinicViewModel.Phone,
                Price = _clinicViewModel.Price,
                ClinicSpecializationId = _clinicViewModel.SpecilizationID,
                Doctors = _clinicViewModel.DoctorsInClinic,
                //Apointments = _clinicViewModel.UpcomingApointments
            };
        }

        public static explicit operator ClinicViewModel(Clinic _clinic)
        {
            return new ClinicViewModel(_clinic);
        }
    }
}
