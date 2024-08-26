using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace common
{
    public class Medications_ActiveSubstance
    {
        private int activeSubstancesID;
        public int ActiveSubstancesID
        {
            get { return activeSubstancesID; }
            set { activeSubstancesID = value; }
        }

        private string medicationCode;
        public string MedicationCode
        {
            get { return medicationCode; }
            set { medicationCode = value; }
        }


        // Additional properties or methods as needed
    }

}
