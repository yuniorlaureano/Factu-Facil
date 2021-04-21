using System;
using System.Collections.Generic;
using System.Text;

namespace FactuFacil.Entity
{
    public class InvoiceDetail: BaseEntityDetails<Guid>
    {
        public string Quantity { get; set; }
        public float Price { get; set; }

        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        public Guid InvoiceId { get; set; }
        public Invoice Invoice { get; set; }
    }
}
