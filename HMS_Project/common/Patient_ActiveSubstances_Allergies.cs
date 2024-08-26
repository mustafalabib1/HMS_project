using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace common
{
    public class Patient_ActiveSubstances_Allergies
    {
        private int patientID;
        public int PatientID
        {
            get { return patientID; }
            set { patientID = value; }
        }

        private int activeSubstancesID;
        public int ActiveSubstancesID
        {
            get { return activeSubstancesID; }
            set { activeSubstancesID = value; }
        }

        // Additional properties or methods as needed
    }
}
