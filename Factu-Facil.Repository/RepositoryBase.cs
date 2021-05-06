using FactuFacil.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FactuFacil.Repository
{
    public abstract class RepositoryBase<T> : IRepository<T> where T : class
    {
        private readonly FactuFacilContext _context;
        private DbSet<T> Entity;


        public RepositoryBase(FactuFacilContext context)
        {
            _context = context;
            Entity = context.Set<T>();
        }

        public void Add(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"La entidad {nameof(T)} es nula");
            }

            Entity.Add(entity);
        }

        public void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentException($"La no se encontro el registro");
            }

            Entity.Remove(entity);
        }

        public async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> expression = null, params Expression<Func<T, object>>[] includes)
        {
            var query = Entity.Where(expression);

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query.Include(include);
                }
            }

            return await query.ToListAsync();
        }

        public async Task<T> GetOne(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes)
        {
            var query = Entity.Where(expression);

            if (includes != null )
            {
                foreach (var include in includes)
                {
                    query.Include(include);
                }
            }

            return await query.FirstOrDefaultAsync() ?? throw new ArgumentException($"No se encontro el registro");
        }

        public void Update(T entity, Func<FactuFacilContext, T> func = null)
        {
            if (entity == null)
            {
                throw new ArgumentException($"No se encontro el registro");
            }

            if (func != null)
            {
                entity = func(_context);
            }

            Entity.Update(entity);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
