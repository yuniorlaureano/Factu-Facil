using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FactuFacil.Web.Models
{
    public class InvoiceViewModel
    {
        public Guid Id { get; set; }
        public string Number { get; set; }
        public decimal Total { get; set; }
        public decimal SubTotal { get; set; }
        public float Isv { get; set; }
        public float Disccount { get; set; }
        public Guid ClientId { get; set; }

        public IEnumerable<InvoiceDetailViewModel> InvoiceDetails { get; set; }
    }
}
