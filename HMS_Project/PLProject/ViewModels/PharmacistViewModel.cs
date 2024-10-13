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
    public class PharmacistViewModel:UserViewModel
    {
        public PharmacistViewModel()
        {
        }
        public PharmacistViewModel(Pharmacist pharmacist)
        {
            Id = pharmacist.Id;
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
            Gender =pharmacist.Gender;
            Id = pharmacist.Id;
        }

        public static explicit operator Pharmacist(PharmacistViewModel pharmacistViewModel)
        {
            return new Pharmacist()
            {
                Id = pharmacistViewModel.Id,
                SSN = pharmacistViewModel.SSN,
                Email = pharmacistViewModel.Email,
                UserPassword = pharmacistViewModel.UserPassword,
                Address = pharmacistViewModel.Address,
                Gender = pharmacistViewModel.Gender,
                Phone = pharmacistViewModel.Phone,
                FullName = $"{pharmacistViewModel.FirstName.Trim()} {pharmacistViewModel.MiddleName.Trim()} {pharmacistViewModel.LastName.Trim()}",
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
