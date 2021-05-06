using FactuFacil.Entity;
using FactuFacil.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FactuFacil.Service
{
    public interface IInventoryService 
    {
        Task<IEnumerable<Inventory>> GetAll(Expression<Func<Inventory, bool>> expression = null);
        Task<Inventory> GetOne(Expression<Func<Inventory, bool>> expression);
        Task Add(Inventory entity);
        Task Update(Inventory entity, Func<FactuFacilContext, Inventory> func = null);
        Task Delete(Inventory entity);
    }

    public class InventoryService : IInventoryService
    {
        private readonly IInventoryRepository _repository;

        public InventoryService(IInventoryRepository repository)
        {
            _repository = repository;
        }

        public Task Add(Inventory entity)
        {
            _repository.Add(entity);
            return _repository.SaveChangesAsync();
        }

        public Task Delete(Inventory entity)
        {
            _repository.Delete(entity);
            return _repository.SaveChangesAsync();
        }

        public Task<IEnumerable<Inventory>> GetAll(Expression<Func<Inventory, bool>> expression = null)
        {
            return _repository.GetAll(expression);
        }

        public Task<Inventory> GetOne(Expression<Func<Inventory, bool>> expression)
        {
            return _repository.GetOne(expression);
        }

        public Task Update(Inventory entity, Func<FactuFacilContext, Inventory> func = null)
        {
            _repository.Update(entity, func);
            return _repository.SaveChangesAsync();
        }
    }
}
