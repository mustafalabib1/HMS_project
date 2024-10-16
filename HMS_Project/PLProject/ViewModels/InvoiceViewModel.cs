using DALProject.model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLProject.ViewModels
{
    public class InvoiceViewModel
    {
        public InvoiceViewModel() { }
        public InvoiceViewModel(Invoice invoice) 
        {
            Id = invoice.Id;
            InvoiceDate = invoice.InvoiceDate;
            TotalAmount = invoice.TotalAmount;
            PaymentStatus = invoice.PaymentStatus;
            PaymentType= Enum.TryParse(invoice.PaymentType, out PaymentType paymentType) ? paymentType : null;
            ReceptionistUserId = invoice.ReceptionistUserId;
            ApointmentId = invoice.ApointmentId;
        }

        public int Id { get; set; }

        [Display(Name = "Invoice Date")]
        [Required(ErrorMessage = " Please Choose a Date.")]
        public DateTime InvoiceDate { get; set; }

        [Display(Name = "Total Amount")]
        [Required(ErrorMessage = " Please Enter Total Amount.")]
        public decimal TotalAmount { get; set; }
        [Display(Name = "Payment Status")]
        [Required(ErrorMessage = " Please Select Status.")]
        public bool PaymentStatus { get; set; }

        [Display(Name = "Payment Type")]
        [Required(ErrorMessage = " Please Select Type.")]
        public PaymentType? PaymentType { get; set; }

        public IEnumerable <Receptionist> ReceptionistsReader { get; set; } = new HashSet<Receptionist>();

        [Display(Name = "Receptionist Name")]
        [Required(ErrorMessage = " Please Select Receptionist Name.")]
        public string? ReceptionistUserId { get; set; }
        public IEnumerable<Apointment>  ApointmentsReader { get; set; } = new HashSet<Apointment>();
        [Display(Name = "Appointment Date")]
        [Required(ErrorMessage = " Please Select Receptionist Name.")]
        public int? ApointmentId { get; set; }



        public static explicit operator Invoice(InvoiceViewModel invoiceViewModel)
        {
            return new Invoice()
            {
                Id = invoiceViewModel.Id,
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
