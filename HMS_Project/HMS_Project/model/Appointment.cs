using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS_Project.model
{
    internal class Appointment
    {
        public int AppointmentID { get; set; } // Identity (Auto-incremented in SQL)

        private DateTime appointmentDate;
        public DateTime AppointmentDate
        {
            get { return appointmentDate; }
            set { appointmentDate = value; }
        }

        private char appointmentStatus;
        public char AppointmentStatus
        {
            get { return appointmentStatus; }
            set { appointmentStatus = value; }
        }

        private int patientID;
        public int PatientID
        {
            get { return patientID; }
            set { patientID = value; }
        }

        private int clinicID;
        public int ClinicID
        {
            get { return clinicID; }
            set { clinicID = value; }
        }

        private int receptionID;
        public int ReceptionID
        {
            get { return receptionID; }
            set { receptionID = value; }
        }
    }
}
