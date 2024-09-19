using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS_Project.model
{
    public class Reception
    {
        public int ReceptionId { get; set; }

        public string Phone { get; set; } = null!;

        public ICollection<Invoice> invoices { get; set; } = new HashSet<Invoice>();
        public ICollection<Receptionist> Receptionists { get; set; } = new HashSet<Receptionist>();
        public ICollection<Apointment> Apointments { get; set; } = new HashSet<Apointment>();

    }
}
