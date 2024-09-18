using HMS_Project.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS_Project.model
{
    public class Pharmacy
    {
        public int pharmacyID;
        public string pharmacyName;
        public string pharmacyPhone;

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

        public ICollection<Pharmacist> Pharmacists { get; set; }
        public ICollection<Prescription> Prescriptions { get; set; }
    
        public ICollection<Medication> Medication { get; set; }
    }
}
