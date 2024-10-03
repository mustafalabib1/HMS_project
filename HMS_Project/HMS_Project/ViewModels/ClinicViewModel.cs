using DALProject.Data.Migrations;
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
            Id = clinic.ClinicId;
            Name = clinic.Name;
            Phone = clinic.Phone;
            Price = clinic.Price;
            Specialization = clinic.Specialization;
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Name is Required")]
        [Display(Name = "Clinic Name")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Phone Number Is Required")]
        [Display(Name = "Phone Number")]
        [Phone(ErrorMessage = "The phone number you entered is invalid.")]
        [MaxLength(11, ErrorMessage = "Phone Number Can be a maximum of 11 numbers")]
        [MinLength(10, ErrorMessage = "Phone Number Can be a minimum of 10 numbers")]
        public string Phone { get; set; } = null!;
        
        public IEnumerable<ClinicSpecializationLookup> SpecializationsDateReader { get; set; } = new HashSet<ClinicSpecializationLookup>();
        
        [Required(ErrorMessage = " Please Choose a Specilization for this Clinic.")]
        public string Specialization { get; set; } = null!;
        
        [Required(ErrorMessage ="Please specify the price.")]
        [Display(Name = "Price per Visit")]
        public double? Price { get; set; }

        //public IEnumerable<Nurse> NursesDateReader { get; set; } = new HashSet<Nurse>();
        //public ICollection<Nurse> NursesInClinic { get; set; } = new HashSet<Nurse>();
        //public IEnumerable<Doctor> DoctorsDateReader { get; set; } = new HashSet<Doctor>();
        //public ICollection<Doctor> DoctorsInClinic { get; set; } = new HashSet<Doctor>();

        //HashSet<Apointment> UpcomingApointments { get; set; } = new HashSet<Apointment>();

        //// Maybe if we want to display the number of Doctors, Nurses and Apointments in the clinic we should use it new view 
        //public int TotalDoctors { get; set; }
        //public int TotalNurses { get; set; }
        //public int TotalApointments { get; set; }


        public static explicit operator Clinic(ClinicViewModel _clinicViewModel)
        {
            return new Clinic()
            {
                Name = _clinicViewModel.Name,
                Phone = _clinicViewModel.Phone,
                Price = _clinicViewModel.Price ?? default,
                Specialization = _clinicViewModel.Specialization,
                //Doctors = _clinicViewModel.DoctorsInClinic,
                //Apointments = _clinicViewModel.UpcomingApointments
            };
        }

        public static explicit operator ClinicViewModel(Clinic _clinic)
        {
            return new ClinicViewModel(_clinic);
        }
    }
}
