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

        public virtual ICollection<Medication> Medication { get; set; } = new HashSet<Medication>();
        public virtual ICollection<Prescription> Prescription { get; set; } = new HashSet<Prescription>();
        public virtual ICollection<ActiveSubstance> ActSbuAllergies { get; set; } = new HashSet<ActiveSubstance>();
        public virtual ICollection<Apointment> Apointments { get; set; } = new HashSet<Apointment>();
        public virtual ICollection<Invoice> Invoices { get; set; } = new HashSet<Invoice>();
        public virtual ICollection<MedicalRecord> MedicalRecords { get; set; } = new HashSet<MedicalRecord>();


    }
}
