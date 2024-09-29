using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALProject.model
{
    public class Receptionist : HmsUser
    {
        #region One2Many With Invoice
        public ICollection<Invoice> invoices { get; set; } = new HashSet<Invoice>();
        #endregion

        #region One2Many With Apiontment 
        public ICollection<Apointment> Apointments { get; set; } = new HashSet<Apointment>();
        #endregion
    }
}
