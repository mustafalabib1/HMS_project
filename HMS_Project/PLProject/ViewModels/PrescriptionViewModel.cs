using DALProject.model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLProject.ViewModels
{
    public class PrescriptionViewModel
    {
        public PrescriptionViewModel() { }
        public PrescriptionViewModel(Prescription prescription) 
        {
            PrescriptionID = prescription.Id;
            PrescriptionItems = (HashSet<PrescriptionItem>)prescription.PrescriptionItems;
            PharmacistId = prescription.PharmacistId;
            ApointmentId = prescription.ApointmentId;
            DoctorId = prescription.DoctorId;
        }

        [Required]
        public int PrescriptionID { get; set; }
        HashSet<PrescriptionItem> PrescriptionItems { get; set; } = new HashSet<PrescriptionItem>();
        public int? PharmacistId { get; set; }
        public int ApointmentId { get; set; }
        public int DoctorId { get; set; }

        public static explicit operator Prescription(PrescriptionViewModel prescriptionViewModel)
        {
            return new Prescription()
            {
                Id = prescriptionViewModel.PrescriptionID,
                PrescriptionItems = prescriptionViewModel.PrescriptionItems,
                PharmacistId = prescriptionViewModel?.PharmacistId,
                ApointmentId = prescriptionViewModel.ApointmentId,
                DoctorId = prescriptionViewModel.DoctorId,
            };
        }

        public static explicit operator PrescriptionViewModel(Prescription prescription)
        {
            return new PrescriptionViewModel(prescription);
        }
    }
}
