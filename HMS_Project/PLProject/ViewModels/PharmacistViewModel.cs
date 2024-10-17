using DALProject.model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace PLProject.ViewModels
{
    public class PharmacistViewModel : UserViewModel
    {
        public PharmacistViewModel()
        {
        }
        public PharmacistViewModel(Pharmacist pharmacist)
        {
            UserId = pharmacist.UserId;
            Id = pharmacist.Id;
            SSN = pharmacist.AppUser.SSN;
            string[] name = pharmacist.AppUser.FullName.Split();
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
            DateOfBirth = pharmacist.AppUser.DateOfBirth;
            Phone = pharmacist.AppUser.PhoneNumber;
            Email = pharmacist.AppUser.Email;
            Address = pharmacist.AppUser.Address;
            Gender =pharmacist.AppUser.Gender;
            Id = pharmacist.Id;
        }

        public static explicit operator Pharmacist(PharmacistViewModel pharmacistViewModel)
        {
            return new Pharmacist()
            {
                UserId = pharmacistViewModel.UserId,
                Id = pharmacistViewModel.Id,
                //SSN = pharmacistViewModel.SSN,
                //Email = pharmacistViewModel.Email,
                //Address = pharmacistViewModel.Address,
                //Gender = pharmacistViewModel.Gender,
                //PhoneNumber = pharmacistViewModel.Phone,
                //FullName = $"{pharmacistViewModel.FirstName.Trim()} {pharmacistViewModel.MiddleName.Trim()} {pharmacistViewModel.LastName.Trim()}",
                //DateOfBirth = pharmacistViewModel.DateOfBirth
            };
        }
        //we shoud make explicit casting Pharmacist to PharmacistViewModel but this is not important in this case 

        public static explicit operator PharmacistViewModel(Pharmacist pharmacist)
        {
            return new PharmacistViewModel(pharmacist);
        }

    }
}
