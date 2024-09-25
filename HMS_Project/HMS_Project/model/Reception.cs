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

        #region One2Many With Invoice
        public ICollection<Invoice> invoices { get; set; } = new HashSet<Invoice>();
        #endregion

        #region One2Many With Recptionist
        public ICollection<Receptionist> Receptionists { get; set; } = new HashSet<Receptionist>();
        #endregion

        #region One2Many With Apintment 
        public ICollection<Apointment> Apointments { get; set; } = new HashSet<Apointment>(); 
        #endregion

    }
}
