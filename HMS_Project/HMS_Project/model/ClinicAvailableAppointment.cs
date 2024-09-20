using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS_Project.model
{
    public class ClinicAvailableAppointment
    {
        public int ClinicId { get; set; }
        public Clinic Clinic { get; set; } = null!;

        public int AvailableAppointmentId { get; set; }
        public AvailableAppointment AvailableAppointment { get; set; } = null!;

        public int AvailableSlots { get; set; }

    }
}
