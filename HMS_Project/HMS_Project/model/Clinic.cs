using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS_Project.model
{
    public class Clinic
    {
        public int ClinicId { get; set; }
        public double Price { get; set; }
        public string Name { get; set; } = null!;
        public string Specilization { get; set; } = null!;
        public string Phone { get; set; } = null!;

        #region One2Many With Nurses
        public ICollection<Nurse> Nurses { get; set; } = new HashSet<Nurse>();
        #endregion

        #region One2Many With Doctors
        public ICollection<Doctor> Doctors { get; set; } = new HashSet<Doctor>();
        #endregion

        #region One2Many With Appointment
        public ICollection<Apointment> Apointments { get; set; } = new HashSet<Apointment>();
        #endregion

        #region Many2Many With AvailableAppointment
        public ICollection<ClinicAvailableAppointment> ClinicAvailableAppointments { get; set; } = new HashSet<ClinicAvailableAppointment>();
        #endregion
    }
}
