using System;
using System.Collections.Generic;
using System.Text;

namespace FactuFacil.Entity
{
    public class Inventory : BaseEntityDetails<Guid>
    {
        public int Amount { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }        
    }
}
