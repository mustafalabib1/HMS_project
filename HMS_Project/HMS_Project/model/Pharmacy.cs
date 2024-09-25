using HMS_Project.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS_Project.model
{
    public class Pharmacy
    {
        public int PharmacyID {  get; set; }
        public string PharmacyName { get; set; } = null!;
        public string Phone { get; set; } = null!;

        #region One2Many With Pharmaist
        public virtual ICollection<Pharmacist> Pharmacists { get; set; } = new HashSet<Pharmacist>();
        #endregion

        #region One2Many With Presciption
        public virtual ICollection<Prescription> Prescriptions { get; set; } = new HashSet<Prescription>(); 
        #endregion
    }
}
