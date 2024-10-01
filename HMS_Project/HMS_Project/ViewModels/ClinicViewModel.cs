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
            Name = clinic.Name;
            Phone = clinic.Phone;
            Price = clinic.Price;
            Specilization = clinic.Specilization;
            DoctorsInClinic = (HashSet<Doctor>)clinic.Doctors;
        }

        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public string Phone { get; set; } = null!;
        [Required]
        public string Specilization { get; set; } = null!;
        [Required]
        public double Price { get; set; }
        public IEnumerable<Nurse> NursesDateReader { get; set; } = new HashSet<Nurse>();
        public ICollection<Nurse> NursesInClinic { get; set; } = new HashSet<Nurse>();
        public IEnumerable<Doctor> DoctorsDateReader { get; set; } = new HashSet<Doctor>();
        public ICollection<Doctor> DoctorsInClinic { get; set; } = new HashSet<Doctor>();


        public static explicit operator Clinic(ClinicViewModel _clinicViewModel)
        {
            return new Clinic()
            {
                Name = _clinicViewModel.Name,
                Phone = _clinicViewModel.Phone,
                Price = _clinicViewModel.Price,
                Specilization = _clinicViewModel.Specilization,
                Doctors = _clinicViewModel.DoctorsInClinic,
            };
        }

        public static explicit operator ClinicViewModel(Clinic _clinic)
        {
            return new ClinicViewModel(_clinic);
        }
    }
}
