using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALProject.model
{
    public class Pharmacist : ModelBase
	{
		// Relation With AppUser
		public string? UserId { get; set; }
		public virtual AppUser AppUser { get; set; }

		#region One2Many With Prescription
		public virtual ICollection<Prescription> Prescriptions { get; set; } = new HashSet<Prescription>();
        #endregion
    }
}
