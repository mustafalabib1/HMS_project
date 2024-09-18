using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace HMS_Project.model
{
    public class Prescription
    {
        public int prescriptionID;
        public int PrescriptionID
        {
            get { return prescriptionID; }
            set { prescriptionID = value; }
        }

        public int dosage;
        public int Dosage
        {
            get { return dosage; }
            set { dosage = value; }
        }

        public DateTime dateIssued;
        public DateTime DateIssued
        {
            get { return dateIssued; }
            set { dateIssued = value; }
        }

        private int duration;
        public int Duration
        {
            get { return duration; }
            set { duration = value; }
        }

        private int doctorID;
        public int DoctorID
        {
            get { return doctorID; }
            set { doctorID = value; }
        }

        public int pharmacyId;
        public int PharmacyId
        {
            get { return pharmacyId; }
            set { pharmacyId = value; }
        }

        public int patientID;
        public int PatientID
        {
            get { return patientID; }
            set { patientID = value; }
        }

        public Pharmacy Pharmacy { get; set; }

        public Patient Patient { get; set; }
    }
}
