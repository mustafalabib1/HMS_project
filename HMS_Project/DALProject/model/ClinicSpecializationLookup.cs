using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALProject.model
{
    public class ClinicSpecializationLookup
    {
        public int? Id { get; set; }
        public string Specialization { get; set; } = null!;

        #region One2Many With Clinic
        public virtual ICollection<Clinic> Clinic { get; set; }=new HashSet<Clinic>();
        #endregion
    }
}
