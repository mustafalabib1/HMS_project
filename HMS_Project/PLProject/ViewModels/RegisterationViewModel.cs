using DALProject.model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLProject.ViewModels
{
    /// <summary>
    /// patient resgastion 
    /// </summary>
    public class RegisterationViewModel
    {
        public RegisterationViewModel()
        {
        }
        public RegisterationViewModel(Patient patient)
        {
            SSN = patient.SSN;
            string[] name = patient.FullName.Split();
            if (name.Length == 3)
            {
                FirstName = name[0];
                MiddleName = name[1];
                LastName = name[2];
            }
            else if (name.Length == 2)
            {
                FirstName = name[0];
                LastName = name[1];
            }
            DateOfBirth = patient.DateOfBirth;
            Phone = patient.Phone;
            Email = patient.Email;
            UserPassword = patient.UserPassword;
            Address = patient.Address;
            Gender = Enum.TryParse(patient.Gender, out Gender Gendervalue) ? Gendervalue : null;
        }
        //public HashSet<DoctorSpecializationLookup> Specializations { get; set; }
        //public string Specializations { get; set; };
        [Required]
        public long SSN { get; set; }
        [Required]
        public string FirstName { get; set; } = null!;
        [Required]
        public string MiddleName { get; set; } = null!;
        [Required]
        public string LastName { get; set; } = null!;
        [Required]
        public DateOnly DateOfBirth { get; set; }
        public string? Phone { get; set; } = null!;
        [EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        public string UserPassword { get; set; } = null!;
        public string? Address { get; set; }
        public Gender? Gender { get; set; }

        public static explicit operator Patient(RegisterationViewModel PatViewModel)
        {
            return new Patient()
            {
                SSN = PatViewModel.SSN,
                Email = PatViewModel.Email,
                UserPassword = PatViewModel.UserPassword,
                Address = PatViewModel.Address,
                Gender = PatViewModel.Gender.ToString(),
                Phone = PatViewModel.Phone,
                FullName = $"{PatViewModel.FirstName.Trim()} {PatViewModel.MiddleName.Trim()} {PatViewModel.LastName.Trim()}",
                DateOfBirth = PatViewModel.DateOfBirth
            };
        }
        //we shoud make explicit casting patient to PatViewModle but this is not important in this case 

        public static explicit operator RegisterationViewModel(Patient patient)
        {
            return new RegisterationViewModel(patient);
        }
    }
}
