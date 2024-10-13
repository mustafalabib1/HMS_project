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
    public class NurseViewModel:UserViewModel
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
            Gender = nurse.Gender;
            ClinicId = nurse.ClinicId;
            Id=nurse.Id;
        }

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
                Gender = nurseViewModel.Gender,
                Phone = nurseViewModel.Phone,
                FullName = $"{nurseViewModel.FirstName.Trim()} {nurseViewModel.MiddleName.Trim()} {nurseViewModel.LastName.Trim()}",
                DateOfBirth = nurseViewModel.DateOfBirth,
                ClinicId = nurseViewModel.ClinicId,
            };
        }

        public static explicit operator NurseViewModel(Nurse nurse)
        {
            return new NurseViewModel(nurse);
        }
    }
}