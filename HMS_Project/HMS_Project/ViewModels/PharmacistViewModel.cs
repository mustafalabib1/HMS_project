using DALProject.model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS_Project.ViewModels
{
    internal class PharmacistViewModel
    {
        public PharmacistViewModel()
        {
        }
        public PharmacistViewModel(Pharmacist pharmacist)
        {
            SSN = pharmacist.SSN;
            string[] name = pharmacist.FullName.Split();
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
            DateOfBirth = pharmacist.DateOfBirth;
            Phone = pharmacist.Phone;
            Email = pharmacist.Email;
            UserPassword = pharmacist.UserPassword;
            Address = pharmacist.Address;
            Gender = Enum.TryParse(pharmacist.Gender, out Gender Gendervalue) ? Gendervalue : null;
        }

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

        public static explicit operator Pharmacist(PharmacistViewModel pharmacistViewModel)
        {
            return new Pharmacist()
            {
                SSN = pharmacistViewModel.SSN,
                Email = pharmacistViewModel.Email,
                UserPassword = pharmacistViewModel.UserPassword,
                Address = pharmacistViewModel.Address,
                Gender = pharmacistViewModel.Gender.ToString(),
                Phone = pharmacistViewModel.Phone,
                FullName = $"{pharmacistViewModel.FirstName} {pharmacistViewModel.MiddleName} {pharmacistViewModel.LastName}",
                DateOfBirth = pharmacistViewModel.DateOfBirth
            };
        }
        //we shoud make explicit casting Pharmacist to PharmacistViewModel but this is not important in this case 

        public static explicit operator PharmacistViewModel(Pharmacist pharmacist)
        {
            return new PharmacistViewModel(pharmacist);
        }

    }
}
