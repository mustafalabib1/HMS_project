using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALProject.model
{
    public class Clinic
    {
        public int ClinicId { get; set; }
        public double Price { get; set; }
        public string Name { get; set; } = null!;
        public string Phone { get; set; } = null!;

        #region One2Many With ClinicSpecializationLookup
        public string Specilization { get; set; } = null!;
        public virtual ClinicSpecializationLookup ClinicSpecilization { get; set; } = null!; 
        #endregion

        #region One2Many With Nurses
        public virtual ICollection<Nurse> Nurses { get; set; } = new HashSet<Nurse>();
        #endregion

        #region One2Many With Doctors
        public virtual ICollection<Doctor> Doctors { get; set; } = new HashSet<Doctor>();
        #endregion

        #region One2Many With Appointment
        public virtual ICollection<Appointment> Appointments { get; set; } = new HashSet<Appointment>();
        #endregion
    }
}
