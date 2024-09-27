using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALProject.model
{
    public class PrescriptionItem
    {
        public int Id { get; set; } 
        public string FullDosage { get; set; } = null!;

        #region One2One With ActiveSubstance
        public virtual int? ActiveSubstanceId { get; set; }
        public virtual ActiveSubstance ActiveSubstance { get; set; } = null!;
        #endregion

        #region One2Many With Prescription
        public virtual int? PrescriptionId { get; set; }
        public virtual Prescription Prescription { get; set; }=null!;
        #endregion

        #region One2Many With Medication
        public virtual ICollection<PrescriptionItemMedication> Medications { get; set; } = new HashSet<PrescriptionItemMedication>();
        #endregion
    }
}
