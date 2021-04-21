using System;
using System.Collections.Generic;
using System.Text;

namespace FactuFacil.Entity
{
    public class Invoice : BaseEntityDetails<Guid>
    {
        public string Number { get; set; }
        public decimal Total { get; set; }
        public decimal SubTotal { get; set; }
        public float Isv { get; set; }
        public float Disccount { get; set; }

        public Guid ClientId { get; set; }
        public Client Client { get; set; }

        public IEnumerable<InvoiceDetail> InvoiceDetails { get; set; }
    }
}
