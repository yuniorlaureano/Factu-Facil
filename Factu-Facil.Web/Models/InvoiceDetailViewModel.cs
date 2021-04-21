using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FactuFacil.Web.Models
{
    public class InvoiceDetailViewModel
    {
        public Guid Id { get; set; }
        public string Quantity { get; set; }
        public float Price { get; set; }
        public Guid ProductId { get; set; }
        public Guid InvoiceId { get; set; }
    }
}
