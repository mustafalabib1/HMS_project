using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS_Project.model
{
    internal class Invoice
    {
       
        public int InvoiceID { get; set; }

        public DateTime InvoiceDate { get; set; }

        public decimal TotalAmount { get; set; }

        public bool PaymentStatus { get; set; }

        public char PaymentType { get; set; }

        //public int PatientID { get; set; }

        //public int ReceptionID { get; set; }
    }
}
