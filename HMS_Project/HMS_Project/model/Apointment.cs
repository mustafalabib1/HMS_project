using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS_Project.model
{
    internal class Apointment
    {
        public  int ApointmentId { get; set; }
        public DateOnly ApointmentDate { get; set; }
        public TimeOnly ApointmentTime { get; set; }
        public char ApointmentStatus { get; set; }
    }
}
