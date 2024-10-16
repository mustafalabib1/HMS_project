using DALProject.model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLProject.ViewModels
{
    public class ReceptionistViewModel : UserViewModel
    {
        public ReceptionistViewModel()
        {
        }
        public ReceptionistViewModel(Receptionist receptionist)
        {
             Id = receptionist.Id;
            SSN = receptionist.AppUser.SSN;
            string[] name = receptionist.AppUser.FullName.Split();
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
            DateOfBirth = receptionist.AppUser.DateOfBirth;
            Phone = receptionist.AppUser.PhoneNumber;
            Email = receptionist.AppUser.Email;
            Address = receptionist.AppUser.Address;
            Gender =receptionist.AppUser.Gender;
            Id = receptionist.Id;
        }
        public static explicit operator Receptionist(ReceptionistViewModel receptionistViewModel)
        {
            return new Receptionist()
            {
                //SSN = receptionistViewModel.SSN,
                //Email = receptionistViewModel.Email,
                //Address = receptionistViewModel.Address,
                //Gender = receptionistViewModel.Gender,
                //PhoneNumber = receptionistViewModel.Phone,
                //FullName = $"{receptionistViewModel.FirstName.Trim()} {receptionistViewModel.MiddleName.Trim()} {receptionistViewModel.LastName.Trim()}",
                //DateOfBirth = receptionistViewModel.DateOfBirth
            };
        }

        public static explicit operator ReceptionistViewModel(Receptionist receptionist)
        {
            return new ReceptionistViewModel(receptionist);
        }

    }
}
