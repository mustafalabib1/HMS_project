using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace common
{
    public class Pharmacy
    {
        private int pharmacyID;
        private string pharmacyName;
        private string pharmacyPhone;

        public int PharmacyID
        {
            get { return pharmacyID; }
            set { pharmacyID = value; }
        }

        public string PharmacyName
        {
            get { return pharmacyName; }
            set { pharmacyName = value; }
        }

        public string PharmacyPhone
        {
            get { return pharmacyPhone; }
            set { pharmacyPhone = value; }
        }
    }
}
