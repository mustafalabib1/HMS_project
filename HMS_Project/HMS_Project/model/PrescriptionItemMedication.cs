using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS_Project.model
{
    public class PrescriptionItemMedication
    {
        public string Dosage { get; set; } = null!;
        public string Duration { get; set; } = null!;

        #region One2Many With Medication 
        public virtual string? MedicationCode { get; set; }
        public virtual Medication Medication { get; set; } = null!;
        #endregion

        #region One2Many With PrescriptionItem
        public virtual int? PrescriptionItemId { get; set; }
        public virtual PrescriptionItem PrescriptionItem { get; set; } = null!; 
        #endregion
    }
}
