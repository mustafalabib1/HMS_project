using DALProject.model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLProject.ViewModels.PrescriptionVM
{
    public class PrescriptionItemDoctorVM
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "Please select an active substance.")]
        public int ActiveSubstanceId { get; set; }
        public virtual ActiveSubstance/*ViewModel*/? ActiveSubstance { get; set; }

        [Required(ErrorMessage = "Please enter the full dosage."), Display(Name = "Full Dosage")]
        public string FullDosage { get; set; } = null!;
        public virtual ICollection<Medication>? MedicationsDatareader { get; set; }
		public virtual List<PreItemMedVM>? Medications { get; set; } = new List<PreItemMedVM>();
	}
}
