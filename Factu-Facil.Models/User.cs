using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace FactuFacil.Entity
{
    public class User: BaseEntity<Guid>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
    }
}
