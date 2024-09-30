using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALProject.model
{
    public class Doctor : HmsUser
    {
        #region One2Many With DoctorSpecializationLookup
        public string Specialization { get; set; } = null!; 
        public virtual DoctorSpecializationLookup DoctorSpecialization { get; set; }=null!;
        #endregion

        #region One2Many With Clinic
        public int? ClinicId { get; set; }
        public virtual Clinic Clinic { get; set; } = null!;
        #endregion

        #region One2Many with Prescription
        public virtual ICollection<Prescription> Prescriptions { get; set; } = new HashSet<Prescription>();
        #endregion

        #region One2Many With DoctorScheduleLookup
        public virtual ICollection<DoctorScheduleLookup> DoctorScheduleLookups { get; set; } = new HashSet<DoctorScheduleLookup>();
        #endregion

        #region One2Many With Appointment
        public virtual ICollection <Appointment> Appointments { get; set; }= new HashSet<Appointment>();
        #endregion
    }
}
