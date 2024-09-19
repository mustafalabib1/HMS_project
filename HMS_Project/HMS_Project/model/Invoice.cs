using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS_Project.model
{
    internal class Invoice
    {
        private int _invoiceID;
        // Full Property for InvoiceID
        public int InvoiceID
        {
            get { return _invoiceID; }
            set { _invoiceID = value; }
        }

        private DateTime _invoiceDate;
        // Full Property for InvoiceDate
        public DateTime InvoiceDate
        {
            get { return _invoiceDate; }
            set { _invoiceDate = value; }
        }

        private decimal _totalAmount;
        // Full Property for TotalAmount
        public decimal TotalAmount
        {
            get { return _totalAmount; }
            set { _totalAmount = value; }
        }

        private bool _paymentStatus;
        // Full Property for PaymentStatus
        public bool PaymentStatus
        {
            get { return _paymentStatus; }
            set { _paymentStatus = value; }
        }

        private char _paymentType;
        // Full Property for PaymentType
        public char PaymentType
        {
            get { return _paymentType; }
            set { _paymentType = value; }
        }

        private int _patientID;
        // Full Property for PatientID
        public int PatientID
        {
            get { return _patientID; }
            set { _patientID = value; }
        }

        private int _receptionID;
        // Full Property for ReceptionID
        public int ReceptionID
        {
            get { return _receptionID; }
            set { _receptionID = value; }
        }
    }
}
