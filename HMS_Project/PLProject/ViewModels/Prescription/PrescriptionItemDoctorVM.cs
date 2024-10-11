using DALProject.model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLProject.ViewModels.Presciption
{
    public class PrescriptionItemDoctorVM
    {
        [Required(ErrorMessage = "Please select an active substance.")]
        public int ActiveSubstanceId { get; set; }

        [Required(ErrorMessage = "Please enter the full dosage.")]
        public string FullDosage { get; set; } = null!;
		//public virtual ICollection<PrescriptionItemMedicationVM>? Medications { get; set; } = new HashSet<PrescriptionItemMedicationVM>();
	}
}
