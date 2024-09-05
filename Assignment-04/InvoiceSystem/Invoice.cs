using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace InvoiceSystem
{
    internal class Invoice
    {
        public Invoice(int invoiceId, DateTime invoiceDate, string name, List<InvoiceDetail> details) 
        {
            // Validate parameters
            if (invoiceId <= 0)
            {
                throw new ArgumentException("Invoice ID must be greater than 0.", nameof(invoiceId));
            }
            if (invoiceDate > DateTime.Now)
            {
                throw new ArgumentException("Invoice date cannot be in the future.", nameof(invoiceId));
            }
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Sales person name cannot be null or empty.", nameof(name));
            }

            // Set values to properties
            _InvoiceId = invoiceId;
            _InvoiceDate = invoiceDate;
            _Name = name;
            InvoiceDetails = details;
        }

        public double InvoiceTotal()
        {
            double total = 0.0d;
            foreach (InvoiceDetail detail in InvoiceDetails)
            {
                total += detail.ExtendedPrice;
            }
            return total;
        }

        public override string ToString()
        {
            return string.Format("\t{0,-4} {1,-10}\t{2,-30}\t{3,15}",
                               _InvoiceId,
                               _InvoiceDate.ToShortDateString(),
                               _Name,
                               string.Format("${0:f2}", InvoiceTotal())
                               );
        }

        public int InvoiceId
        {
            get { return _InvoiceId; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Invoice ID must be greater than 0.", nameof(InvoiceId));
                }
                else
                {
                    _InvoiceId = value;
                }
            } 
        }

        public DateTime InvoiceDate
        {
            get { return _InvoiceDate; }
            set
            {
                if (value > DateTime.Now)
                {
                    throw new ArgumentException("Invoice date cannot be in the future.", nameof(InvoiceDate));
                }
                else
                {
                    _InvoiceDate = value;
                }
            }
        }

        public string Name
        {
            get { return _Name; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Sales person name cannot be null or empty.", nameof(Name));
                }
                else
                {
                    _Name = value.Trim();
                }
            }
        }

        public List<InvoiceDetail> InvoiceDetails { get; set; }

        private int _InvoiceId;
        private DateTime _InvoiceDate;
        private string _Name;
    }
}
