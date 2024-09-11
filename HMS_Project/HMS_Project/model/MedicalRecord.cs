using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS_Project.model
{
    internal class MedicalRecord
    {
        private int _recordID;
        // Full property for RecordID
        public int RecordID
        {
            get { return _recordID; }
            set { _recordID = value; }
        }


        private string _diagnosis;
        // Full property for Diagnosis
        public string Diagnosis
        {
            get { return _diagnosis; }
            set { _diagnosis = value; }
        }

        private DateTime _createdDate;
        // Full property for CreatedDate
        public DateTime CreatedDate
        {
            get { return _createdDate; }
            set { _createdDate = value; }
        }

        private string _labResults;
        // Full property for LabResults
        public string LabResults
        {
            get { return _labResults; }
            set { _labResults = value; }
        }

        private int _patientID;
        // Full property for PatientID
        public int PatientID
        {
            get { return _patientID; }
            set { _patientID = value; }

        }
    }
}
