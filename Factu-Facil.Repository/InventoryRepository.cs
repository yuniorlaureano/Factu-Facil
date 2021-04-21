using System;
using System.Collections.Generic;
using System.Text;
using FactuFacil.Entity;
using FactuFacil.Repository;

namespace FactuFacil.Repository
{
    public interface IInventoryRepository : IRepository<Inventory>
    {

    }

    public class InventoryRepository : RepositoryBase<Inventory>, IInventoryRepository
    {
        public InventoryRepository(FactuFacilContext context) : base(context) 
        {
        
        }
    }
}
