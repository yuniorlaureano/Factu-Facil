using System;
using System.Collections.Generic;
using System.Text;

namespace FactuFacil.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float SalePrice { get; set; }
        public float PurchasePrice { get; set; }
        public string Code { get; set; }
    }
}
