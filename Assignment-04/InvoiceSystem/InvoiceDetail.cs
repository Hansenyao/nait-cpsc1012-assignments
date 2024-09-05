using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace InvoiceSystem
{
    internal class InvoiceDetail
    {
        public InvoiceDetail(string productId, string description, int quantity, double price) 
        {
            // Validate parameters
            if (string.IsNullOrWhiteSpace(productId))
            {
                throw new ArgumentException("Product ID cannot be null or white spaces.", nameof(productId));
            }
            if (string.IsNullOrWhiteSpace(description))
            {
                throw new ArgumentException("Product description cannot be null or white spaces.", nameof(description));
            }
            if (quantity <= 0)
            {
                throw new ArgumentException("Quantity must be greater than 0.", nameof(quantity));
            }
            if (price < 0)
            {
                throw new ArgumentException("Price must be greater or equal to 0.", nameof(price));
            }

            // Set values to properties
            _ProductId = productId.Trim();
            _Description = description.Trim();
            _Quantity = quantity;
            _Price = price;
        }

        public override string ToString()
        {
            return string.Format("{0,-10}\t{1,-40}\t{2,10}\t{3,10}\t{4,10}",
                                 _ProductId,  
                                 _Description,
                                 _Quantity,
                                 string.Format("${0:f2}", _Price),
                                 string.Format("${0:f2}", ExtendedPrice)
                                 );
        }

        public string ProductId
        {
            get { return _ProductId; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Product ID cannot be null or white spaces.", nameof(ProductId));
                }
                else
                {
                    _ProductId = value.Trim();
                }
            }
        }

        public string Description
        {
            get { return _Description; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Product description cannot be null or white spaces.", nameof(Description));
                }
                else
                {
                    _Description = value.Trim();
                }
            }
        }

        public int Quantity
        {
            get { return _Quantity; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Quantity must be greater than 0.", nameof(Quantity));
                }
                else
                {
                    _Quantity = value;
                }
            }
        }

        public double Price
        {
            get { return _Price; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Price must be greater or equal to 0.", nameof(Price));
                }
                else
                {
                    _Price = value;
                }
            }
        }

        public double ExtendedPrice
        {
            get { return _Quantity * _Price; }
        }

        private string _ProductId; 
        private string _Description;
        private int _Quantity;
        private double _Price;
    }
}
