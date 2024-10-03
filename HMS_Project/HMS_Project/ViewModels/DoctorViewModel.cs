using DALProject.model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS_Project.ViewModels
{
    public class DoctorViewModel
    {

        public DoctorViewModel()
        {
        }
        public DoctorViewModel(Doctor doctor)
        {
            SSN = doctor.SSN;
            string[] name = doctor.FullName.Split();
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
            DateOfBirth = doctor.DateOfBirth;
            Phone = doctor.Phone;
            Email = doctor.Email;
            UserPassword = doctor.UserPassword;
            Address = doctor.Address;
            Gender = Enum.TryParse(doctor.Gender, out Gender Gendervalue) ? Gendervalue : null;
            specialization = doctor.Specialization;
            //ClinicId=doctor.ClinicId;
            //Schedule = doctor.DoctorScheduleLookups;
        }
        
        public HashSet<DoctorSpecializationLookup>? SpecializationsDateReader { get; set; }
        public string? specialization { get; set; }
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
        //public ICollection<DoctorScheduleLookup> Schedule { get; set; } = new HashSet<DoctorScheduleLookup>();
        //public ICollection<Clinic> ClinicDateReader { get; set; } = new HashSet<Clinic>();
        //public int? ClinicId { get; set; }


        public static explicit operator Doctor(DoctorViewModel doctorViewModwl)
        {
            return new Doctor()
            {
                SSN = doctorViewModwl.SSN,
                Email = doctorViewModwl.Email,
                UserPassword = doctorViewModwl.UserPassword,
                Address = doctorViewModwl.Address,
                Gender = doctorViewModwl.Gender.ToString(),
                Phone = doctorViewModwl.Phone,
                FullName = $"{doctorViewModwl.FirstName} {doctorViewModwl.MiddleName} {doctorViewModwl.LastName}",
                DateOfBirth = doctorViewModwl.DateOfBirth,
                Specialization= doctorViewModwl.specialization,
                //ClinicId = doctorViewModwl.ClinicId,
            };
        }
        //we shoud make explicit casting Doctor to DoctorViewModle but this is not important in this case 

        public static explicit operator DoctorViewModel(Doctor doctor)
        {
            return new DoctorViewModel(doctor);
        }
    }
}
