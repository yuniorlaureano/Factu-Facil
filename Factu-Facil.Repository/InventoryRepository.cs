using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FactuFacil.Entity;
using Microsoft.EntityFrameworkCore;

namespace FactuFacil.Repository
{
    public interface IInventoryRepository
    {
        Task<IEnumerable<Inventory>> GetAll(Expression<Func<Inventory, bool>> expression = null);
        Task<Inventory> GetOne(Expression<Func<Inventory, bool>> expression);
        void Add(Inventory entity);
        void Update(Inventory entity, Func<FactuFacilContext, Inventory> func = null);
        void Delete(Inventory entity);
        Task<int> SaveChangesAsync();
    }

    public class InventoryRepository : IInventoryRepository
    {
        private readonly FactuFacilContext _context;

        public InventoryRepository(FactuFacilContext context) 
        {
            _context = context;
        }

        public void Add(Inventory inventory)
        {
            if (inventory == null)
            {
                throw new ArgumentNullException($"La entidad {nameof(Inventory)} es nula");
            }

            _context.Add(inventory);
        }

        public void Delete(Inventory inventory)
        {
            if (inventory == null)
            {
                throw new ArgumentException($"La no se encontro el registro");
            }

            _context.Remove(inventory);
        }

        public async Task<IEnumerable<Inventory>> GetAll(Expression<Func<Inventory, bool>> expression = null)
        {
            var query = _context.Inventorie
                                .Include(p => p.Product)
                                .Include(p => p.CreatedBy)
                                .Include(p => p.UpdatedBy)
                                .Where(expression);

            return await query.ToListAsync();
        }

        public async Task<Inventory> GetOne(Expression<Func<Inventory, bool>> expression)
        {
            var query = _context.Inventorie
                                .Include(p => p.Product)
                                .Include(p => p.CreatedBy)
                                .Include(p => p.UpdatedBy)
                                .Where(expression);


            return await query.FirstOrDefaultAsync() ?? throw new ArgumentException($"No se encontro el registro");
        }

        public void Update(Inventory inventory, Func<FactuFacilContext, Inventory> func = null)
        {
            if (inventory == null)
            {
                throw new ArgumentException($"No se encontro el registro");
            }

            if (func != null)
            {
                inventory = func(_context);
            }

            _context.Update(inventory);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
