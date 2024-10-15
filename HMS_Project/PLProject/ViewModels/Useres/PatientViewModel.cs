using DALProject.model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLProject.ViewModels
{
    /// <summary>
    /// patient resgastion 
    /// </summary>
    public class PatientViewModel:UserViewModel
    {
        public PatientViewModel()
        {
        }
        public PatientViewModel(Patient patient)
        {
            SSN = patient.SSN;
            string[] name = patient.FullName.Split();
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
            DateOfBirth = patient.DateOfBirth;
            Email = patient.Email;
            Address = patient.Address;
            Gender = patient.Gender;
            Id= patient.Id;
        }

        public static explicit operator Patient(PatientViewModel PatViewModel)
        {
            return new Patient()
            {
                SSN = PatViewModel.SSN,
                Email = PatViewModel.Email,
                Address = PatViewModel.Address,
                Gender = PatViewModel.Gender ?? DALProject.model.Gender.Male,
                FullName = $"{PatViewModel.FirstName.Trim()} {PatViewModel.MiddleName.Trim()} {PatViewModel.LastName.Trim()}",
                DateOfBirth = PatViewModel.DateOfBirth
            };
        }
        //we shoud make explicit casting patient to PatViewModle but this is not important in this case 

        public static explicit operator PatientViewModel(Patient patient)
        {
            return new PatientViewModel(patient);
        }
    }
}
