using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALProject.model
{
    public class Invoice
    {
        public int InvoiceID { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal TotalAmount { get; set; }
        public bool PaymentStatus { get; set; }
        public char PaymentType { get; set; }

        #region One2Many With Receptionist
        public long? ReceptionistId { get; set; }
        public virtual Receptionist Receptionist { get; set; } = null!;
        #endregion

        #region One2One With Apointment
        public int? ApointmentId { get; set; }
        public virtual Apointment Apointment { get; set; }=null!;
        #endregion
    }
}
