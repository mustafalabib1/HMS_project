using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS_Project.model
{
    public class Doctor : HmsUser
    {
        public int DoctorId { get; set; }
        public string Specialization { get; set; } = null!;

        // One2Many With Clinic
        public int ClinicId { get; set; }
        public Clinic Clinic { get; set; } = null!;

        // One2Many with Prescription
        public ICollection<Prescription> Prescriptions { get; set; } = new HashSet<Prescription>();

        // Many2Many with MedicalRecord
        public ICollection<MedicalRecord> MedicalRecords { get; set; } = new HashSet<MedicalRecord>();

        // Many2Many with AvailableAppointment
        public ICollection<AvailableAppointment> AvailableAppointments { get; set; } = new HashSet<AvailableAppointment>();
    }
}
