using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS_Project.model
{
    public class Nurse : HmsUser
    {
        public int NurseId { get; set; }


        // One2Many With Clinic
        public int ClinicId { get; set; }
        public Clinic Clinic { get; set; } = null!;
    }
}
