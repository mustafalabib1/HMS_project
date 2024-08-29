using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace common
{
    public class Pharmacist
    {
        private int pharmacistID;
        public int PharmacistID
        {
            get { return pharmacistID; }
            set { pharmacistID = value; }
        }

        private int pharmacyId;
        public int PharmacyId
        {
            get { return pharmacyId; }
            set { pharmacyId = value; }
        }

        private long ssn;
        public long SSN
        {
            get { return ssn; }
            set { ssn = value; }
        }
    }
}
