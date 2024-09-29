using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALProject.model
{
    public class DoctorSpecializationLookup
    {
        public string Specialization { get; set; } = null!;
        #region One2Many With Doctor
        public virtual ICollection<Doctor> Doctors { get; set; } = new HashSet<Doctor>();
        #endregion
    }
}
