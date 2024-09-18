using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS_Project.model
{
    public class Patient : HmsUser
    {

        public int PatientId { get; set; }

        public string? PatAddress { get; set; }
    }
}
