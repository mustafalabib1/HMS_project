using DALProject.model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS_Project.ViewModels
{
    public class ReceptionistViewModel
    {
        public ReceptionistViewModel()
        {
        }
        public ReceptionistViewModel(Receptionist receptionist)
        {

            SSN = receptionist.SSN;
            string[] name = receptionist.FullName.Split();
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
            DateOfBirth = receptionist.DateOfBirth;
            Phone = receptionist.Phone;
            Email = receptionist.Email;
            UserPassword = receptionist.UserPassword;
            Address = receptionist.Address;
            Gender = Enum.TryParse(receptionist.Gender, out Gender Gendervalue) ? Gendervalue : null;
        }
        public int Id { get; set; }
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

        public static explicit operator Receptionist(ReceptionistViewModel receptionistViewModel)
        {
            return new Receptionist()
            {
                SSN = receptionistViewModel.SSN,
                Email = receptionistViewModel.Email,
                UserPassword = receptionistViewModel.UserPassword,
                Address = receptionistViewModel.Address,
                Gender = receptionistViewModel.Gender.ToString(),
                Phone = receptionistViewModel.Phone,
                FullName = $"{receptionistViewModel.FirstName.Trim()} {receptionistViewModel.MiddleName.Trim()} {receptionistViewModel.LastName.Trim()}",
                DateOfBirth = receptionistViewModel.DateOfBirth
            };
        }
        //we shoud make explicit casting Receptionist to ReceptionistViewModel but this is not important in this case 

        public static explicit operator ReceptionistViewModel(Receptionist receptionist)
        {
            return new ReceptionistViewModel(receptionist);
        }

    }
}
