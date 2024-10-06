using DALProject.model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS_Project.ViewModels
{
    public class DoctorApointmentViewModel
    {
        public DoctorApointmentViewModel()
        {
        }
        public DoctorApointmentViewModel(Apointment apointment)
        {
            ApointmentId=apointment.Id;
            Patient = apointment.Patient;
        }
        public int ApointmentId { get; set; }
        [Required]
        public string Examination { get; set; } = null!;
        public Patient Patient { get; set; } = null!;
        public PrescriptionViewModel? Prescription { get; set; }

        public static explicit operator Apointment(DoctorApointmentViewModel doctorViewModel)
        {
            return new Apointment
            {
				Id = doctorViewModel.ApointmentId,
                Examination = doctorViewModel.Examination,
                // Assuming the `Prescription` property can be assigned
                //Prescription = (Prescription)Prescription
            };
        }
        public static explicit operator DoctorApointmentViewModel(Apointment apointment)
        {
            return new DoctorApointmentViewModel(apointment);
        }
    }
}
