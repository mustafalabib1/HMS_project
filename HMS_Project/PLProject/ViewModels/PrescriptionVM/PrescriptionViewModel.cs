using DALProject.model;
using Microsoft.CodeAnalysis.Completion;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLProject.ViewModels.PrescriptionVM
{
    public class PrescriptionViewModel
    {
		public int prescriptionId { get; set; }
		public virtual List<PrescriptionItemDoctorVM> PrescriptionItems { get; set; } = new List<PrescriptionItemDoctorVM>();

        // Custom validation to check if at least one item is added
        public bool HasItems => PrescriptionItems != null && PrescriptionItems.Any();

        public int? PharmacistId { get; set; }
		[Required]
		public int DoctorId { get; set; }
		public Patient? Patient { get; set; }
	}
}
