using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS_Project.model
{
    public class Pharmacist : HmsUser
    {
        private int pharmacistID;
        public int PharmacistID
        {
            get { return pharmacistID; }
            set { pharmacistID = value; }
        }
        public Pharmacy Pharmacy { get; set; } = null!;

        
    }
}
