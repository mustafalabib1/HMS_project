using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS_Project.model
{
    public class Invoice
    {
        public int InvoiceID { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal TotalAmount { get; set; }
        public bool PaymentStatus { get; set; }
        public char PaymentType { get; set; }

        #region One2Many With Reception
        public int? ReceptionId { get; set; }
        public virtual Reception Reception { get; set; } = null!;
        #endregion

        #region One2One With Apointment
        public int? ApointmentId { get; set; }
        public virtual Apointment Apointment { get; set; }=null!;
        #endregion
    }
}
