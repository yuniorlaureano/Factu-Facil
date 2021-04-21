using FactuFacil.Entity;
using FactuFacil.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace FactuFacil.Service
{
    public interface IInventoryService : IBaseService<Inventory>
    {
    }

    public class InventoryService : BaseService<Inventory>, IInventoryService
    {
        public InventoryService(IInventoryRepository repository) : base(repository)
        {
        }
    }
}
