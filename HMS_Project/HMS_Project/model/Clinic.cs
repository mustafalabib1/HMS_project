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
        public ICollection<Nurse> Nurses { get; set; } = new HashSet<Nurse>();
        public ICollection<Doctor> Doctors { get; set; } = new HashSet<Doctor>();
        public ICollection<Apointment> Apointments { get; set; } = new HashSet<Apointment>();

        public ICollection<ClinicAvailableAppointment> ClinicAvailableAppointments { get; set; } = new HashSet<ClinicAvailableAppointment>();

    }
}
