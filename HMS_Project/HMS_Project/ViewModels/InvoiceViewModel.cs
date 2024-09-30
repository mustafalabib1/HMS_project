using DALProject.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS_Project.ViewModels
{
    internal class InvoiceViewModel
    {
        public InvoiceViewModel() { }
        public InvoiceViewModel(Invoice invoice) 
        {
            InvoiceID = invoice.InvoiceID;
            InvoiceDate = invoice.InvoiceDate;
            TotalAmount = invoice.TotalAmount;
            PaymentStatus = invoice.PaymentStatus;
            PaymentType= Enum.TryParse(invoice.PaymentType, out PaymentType paymentType) ? paymentType : null;
        }

        public int InvoiceID { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal TotalAmount { get; set; }
        public bool PaymentStatus { get; set; }
        public PaymentType? PaymentType { get; set; }

        public static explicit operator Invoice(InvoiceViewModel invoiceViewModel)
        {
            return new Invoice()
            {
                InvoiceID = invoiceViewModel.InvoiceID,
                InvoiceDate = invoiceViewModel.InvoiceDate,
                TotalAmount = invoiceViewModel.TotalAmount,
                PaymentStatus = invoiceViewModel.PaymentStatus,
                PaymentType = invoiceViewModel.PaymentType.ToString()
            };
        }

        public static explicit operator InvoiceViewModel(Invoice invoice)
        {
            return new InvoiceViewModel(invoice);
        }



    }
}
