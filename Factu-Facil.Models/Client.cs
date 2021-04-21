using System;
using System.Collections.Generic;
using System.Text;

namespace FactuFacil.Entity
{
    public class Client : BaseEntityDetails<Guid>
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string IdentificationCard { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}
