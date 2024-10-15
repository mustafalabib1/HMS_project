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
            Email = receptionist.Email;
            Address = receptionist.Address;
            Gender =receptionist.Gender;
            Id = receptionist.Id;
        }
        public static explicit operator Receptionist(ReceptionistViewModel receptionistViewModel)
        {
            return new Receptionist()
            {
                SSN = receptionistViewModel.SSN,
                Email = receptionistViewModel.Email,
                Address = receptionistViewModel.Address,
                Gender = receptionistViewModel.Gender ?? DALProject.model.Gender.Male,
                FullName = $"{receptionistViewModel.FirstName.Trim()} {receptionistViewModel.MiddleName.Trim()} {receptionistViewModel.LastName.Trim()}",
                DateOfBirth = receptionistViewModel.DateOfBirth
            };
        }

        public static explicit operator ReceptionistViewModel(Receptionist receptionist)
        {
            return new ReceptionistViewModel(receptionist);
        }

    }
}
