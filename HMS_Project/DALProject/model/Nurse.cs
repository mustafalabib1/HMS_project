using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALProject.model
{
    public class Nurse : HmsUser
    {
		// Relation With AppUser
		public string? UserId { get; set; }
		public virtual AppUser AppUser { get; set; }

		#region One2Many With Clinic
		public int? ClinicId { get; set; }
        public virtual Clinic Clinic { get; set; } = null!;
        #endregion 
    }
}
