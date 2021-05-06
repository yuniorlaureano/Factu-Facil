using System;
using System.Collections.Generic;
using System.Text;

namespace FactuFacil.Models
{
    public class Inventory
    {
        public Guid Id { get; set; }
        public int Amount { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
    }
}
