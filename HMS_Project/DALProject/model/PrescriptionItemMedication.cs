using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALProject.model
{
    public class PrescriptionItemMedication
    {
        public string Dosage { get; set; } = null!;
        public string Duration { get; set; } = null!;

        #region One2Many With Medication 
        public int? MedicationId{ get; set; }
        public virtual Medication Medication { get; set; } = null!;
        #endregion

        #region One2Many With PrescriptionItem
        public virtual int? PrescriptionItemId { get; set; }
        public virtual PrescriptionItem PrescriptionItem { get; set; } = null!; 
        #endregion
    }
}
