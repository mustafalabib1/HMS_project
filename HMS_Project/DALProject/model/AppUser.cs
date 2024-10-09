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
        public int SSN { get; set; }
        public string FullName { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public DateOnly DateOfBirth { get; set; }
    }
}
