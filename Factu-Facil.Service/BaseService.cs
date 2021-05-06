using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using FactuFacil.Entity;
using FactuFacil.Repository;

namespace FactuFacil.Service
{
    public interface IBaseService<T>
    {
        Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> expression = null, params Expression<Func<T, object>>[] includes);
        Task<T> GetOne(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes);
        Task Add(T entity);
        Task Update(T entity, Func<FactuFacilContext, T> func = null);
        Task Delete(T entity);
    }

    public class BaseService<T> : IBaseService<T> where T: class
    {
        public readonly IRepository<T> _repository;

        public BaseService(IRepository<T> repository)
        {
            _repository = repository;
        }

        public Task Add(T entity)
        {
            _repository.Add(entity);
            return _repository.SaveChangesAsync();
        }

        public Task Delete(T entity)
        {
            _repository.Delete(entity);
            return _repository.SaveChangesAsync();
        }

        public  Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> expression = null, params Expression<Func<T, object>>[] includes)
        {
            return _repository.GetAll(expression, includes);
        }

        public Task<T> GetOne(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes)
        {
            return _repository.GetOne(expression, includes);
        }

        public Task Update(T entity, Func<FactuFacilContext, T> func = null)
        {
            _repository.Update(entity, func);
            return _repository.SaveChangesAsync();
        }
    }
}
