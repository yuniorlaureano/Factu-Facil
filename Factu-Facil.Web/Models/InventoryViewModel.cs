using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FactuFacil.Web.Models
{
    public class InventoryViewModel
    {
        public Guid Id { get; set; }
        public int Amount { get; set; }
        public Guid ProductId { get; set; }
        public ProductViewModel Product { get; set; }
    }
}
