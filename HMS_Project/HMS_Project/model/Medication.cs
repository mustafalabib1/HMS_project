using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS_Project.model
{
    public class Medication
    {
        private string medicationCode;
        public string MedicationCode
        {
            get { return medicationCode; }
            set { medicationCode = value; }
        }

        private string medName;
        public string MedName
        {
            get { return medName; }
            set { medName = value; }
        }

        private int strength;
        public int Strength
        {
            get { return strength; }
            set { strength = value; }
        }

        private int pharmacyId;
        public int PharmacyId
        {
            get { return pharmacyId; }
            set { pharmacyId = value; }
        }

        // Additional properties or methods as needed
    }
}
