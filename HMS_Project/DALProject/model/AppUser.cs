using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALProject.model
{
    public class AppUser : IdentityUser
    {
        // From IdentityUser we have Id, UserName, PasswordHash, Email, PhoneNumber , etc.
        // adding the rest of HMS user properties
        public long SSN { get; set; }
        public string FullName { get; set; } = null!;
        public string? Address { get; set; } = null!;
        public string? Gender { get; set; } = null!;
        public DateOnly DateOfBirth { get; set; }

        // Relation with Users
        public virtual Doctor Doctor { get; set; }
		public virtual Receptionist Receptionist { get; set; }
		public virtual Patient Patient{ get; set; }
		public virtual Nurse Nurse { get; set; }
		public virtual Pharmacist Pharmacist { get; set; }
	}
}
