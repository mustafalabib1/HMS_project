using DALProject.model;
using PLProject.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace PLProject.ViewModels
{
    public class NurseViewModel : UserViewModel
    {
        public NurseViewModel()
        {
        }
        public NurseViewModel(Nurse nurse)
        {
            UserId = nurse.UserId;
            Id = nurse.Id;
            string[] name = nurse.AppUser.FullName.Split();
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
            DateOfBirth = nurse.AppUser.DateOfBirth;
            Phone = nurse.AppUser.PhoneNumber;
            Email = nurse.AppUser.Email;
            Address = nurse.AppUser.Address;
            Gender = nurse.AppUser.Gender;
            ClinicId = nurse.ClinicId;
            Id=nurse.Id;
        }

        public ICollection<Clinic> ClinicDateReader { get; set; } = new HashSet<Clinic>();
        public int? ClinicId { get; set; }

        public static explicit operator Nurse(NurseViewModel nurseViewModel)
        {
            var nurse = new Nurse();
            nurse.UserId = nurseViewModel.UserId;
            nurse.Id = nurseViewModel.Id;
            nurse.AppUser.SSN = nurseViewModel.SSN;
            nurse.AppUser.Email = nurseViewModel.Email;
            nurse.AppUser.Address = nurseViewModel.Address;
            nurse.AppUser.Gender = nurseViewModel.Gender;
            nurse.AppUser.PhoneNumber = nurseViewModel.Phone;
            nurse.AppUser.FullName = $"{nurseViewModel.FirstName.Trim()} {nurseViewModel.MiddleName.Trim()} {nurseViewModel.LastName.Trim()}";
            nurse.AppUser.DateOfBirth = nurseViewModel.DateOfBirth;
            nurse.ClinicId = nurseViewModel.ClinicId;

            return nurse;
        }

        public static explicit operator NurseViewModel(Nurse nurse)
        {
            return new NurseViewModel(nurse);
        }
    }
}