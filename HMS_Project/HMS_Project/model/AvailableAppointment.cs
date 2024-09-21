using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS_Project.model
{
    public class AvailableAppointment
    {
        public int AvailableAppointmentId { get; set; }
        public int ShiftNumber { get; set; }
        public DateTime Date { get; set; }

        #region Many2Many With Doctor
        public ICollection<Doctor> Doctors = new HashSet<Doctor>();
        #endregion

        #region Many2Many With Clinic
        public ICollection<ClinicAvailableAppointment> ClinicAvailableAppointments { get; set; } = new HashSet<ClinicAvailableAppointment>();
        #endregion
    }
}
