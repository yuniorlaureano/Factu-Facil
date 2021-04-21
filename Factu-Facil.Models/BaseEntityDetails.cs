using System;
using System.Collections.Generic;
using System.Text;

namespace FactuFacil.Entity
{
    public class BaseEntityDetails<Y> : BaseEntity<Y>
    {
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public Guid CreatedById { get; set; }
        public User CreatedBy { get; set; }

        public Guid UpdatedById { get; set; }
        public User UpdatedBy { get; set; }
    }
}
