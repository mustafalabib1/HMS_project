using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS_Project.model
{
    public class Pharmacist : HmsUser
    {
        #region One2Many with Pharmacy
        public virtual int? PharmacyId { get; set; }
        public virtual Pharmacy Pharmacy { get; set; } = null!; 
        #endregion
    }
}
