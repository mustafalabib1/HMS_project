using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace common
{
    public class Patient_Medications
    {
        private DateTime dateIssued;
        public DateTime DateIssued
        {
            get { return dateIssued; }
            set { dateIssued = value; }
        }

        private int patientID;
        public int PatientID
        {
            get { return patientID; }
            set { patientID = value; }
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
