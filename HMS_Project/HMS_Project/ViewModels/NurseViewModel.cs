using DALProject.model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace HMS_Project.ViewModels
{
    public class NurseViewModel
    {
        public NurseViewModel()
        {
        }
        public NurseViewModel(Nurse nurse)
        {
            Id = nurse.Id;
            string[] name = nurse.FullName.Split();
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
            DateOfBirth = nurse.DateOfBirth;
            Phone = nurse.Phone;
            Email = nurse.Email;
            UserPassword = nurse.UserPassword;
            Address = nurse.Address;
            Gender = Enum.TryParse(nurse.Gender, out Gender Gendervalue) ? Gendervalue : null;
            ClinicId = nurse.ClinicId;
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
        public ICollection<Clinic> ClinicDateReader { get; set; } = new HashSet<Clinic>();
        public int? ClinicId { get; set; }

        public static explicit operator Nurse(NurseViewModel nurseViewModel)
        {
            return new Nurse()
            {
                SSN = nurseViewModel.SSN,
                Email = nurseViewModel.Email,
                UserPassword = nurseViewModel.UserPassword,
                Address = nurseViewModel.Address,
                Gender = nurseViewModel.Gender.ToString(),
                Phone = nurseViewModel.Phone,
                FullName = $"{nurseViewModel.FirstName.Trim()} {nurseViewModel.MiddleName.Trim()} {nurseViewModel.LastName.Trim()}",
                DateOfBirth = nurseViewModel.DateOfBirth,
                ClinicId = nurseViewModel.ClinicId,
            };
        }
        //we shoud make explicit casting Nurse to NurseViewModel but this is not important in this case 

        public static explicit operator NurseViewModel(Nurse nurse)
        {
            return new NurseViewModel(nurse);
        }
    }
}