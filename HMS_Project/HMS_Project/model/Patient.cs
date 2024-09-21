using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS_Project.model
{
    public class Patient : HmsUser
    {

        public int PatientId { get; set; }
        public string? PatAddress { get; set; }

        public virtual ICollection<PatientMedication> PatientMedication { get; set; } = new HashSet<PatientMedication>();

        #region One2Many With Prescription
        public virtual ICollection<Prescription> Prescriptions { get; set; } = new HashSet<Prescription>();
        #endregion

        public virtual ICollection<ActiveSubstance> ActSbuAllergies { get; set; } = new HashSet<ActiveSubstance>();

        #region One2Many With Appointment
        public virtual ICollection<Apointment> Apointments { get; set; } = new HashSet<Apointment>();
        #endregion

        public virtual ICollection<Invoice> Invoices { get; set; } = new HashSet<Invoice>();

        #region One2Many With MedicalRecord
        public virtual ICollection<MedicalRecord> MedicalRecords { get; set; } = new HashSet<MedicalRecord>();
        #endregion
    }
}
