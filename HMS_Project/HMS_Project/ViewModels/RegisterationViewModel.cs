using DALProject.model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS_Project.ViewModels
{
    /// <summary>
    /// patient resgastion 
    /// </summary>
    internal class RegisterationViewModel
    {
        public RegisterationViewModel()
        {
        }
        public RegisterationViewModel(Patient patient)
        {
            SSN = patient.SSN;
            //FirstName = firstName;
            //MidleName = midleName;
            //LastName = patient.FullName;
            DateOfBirth = patient.DateOfBirth;
            Phone = patient.Phone;
            Email = patient.Email;
            UserPassword = patient.UserPassword;
            Address = patient.Address;
            Gender = Enum.TryParse(patient.Gender, out Gender Gendervalue) ? Gendervalue : null;
        }

        [Required]
        public long SSN { get; set; }
        //public FullName FullName { get; set; } = null!;

        [Required]
        public string FirstName { get; set; } = null!;
        [Required]
        public string MidleName { get; set; } = null!;
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
                FullName = $"{PatViewModel.FirstName} {PatViewModel.MidleName} {PatViewModel.LastName}",
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
