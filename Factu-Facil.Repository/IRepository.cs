using FactuFacil.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FactuFacil.Repository
{
    public interface IRepository<T> where T: class
    {
        Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> expression = null, params Expression<Func<T, object>>[] includes);
        Task<T> GetOne(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes);
        void Add(T entity);
        void Update(T entity, Func<FactuFacilContext, T> func = null);
        void Delete(T entity);
        Task<int> SaveChangesAsync();
    }
}
