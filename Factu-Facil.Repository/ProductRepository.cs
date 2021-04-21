using System;
using System.Collections.Generic;
using System.Text;
using FactuFacil.Entity;
using FactuFacil.Repository;

namespace FactuFacil.Repository
{
    public interface IProductRepository : IRepository<Product>
    {

    }

    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(FactuFacilContext context) : base(context) 
        {
        
        }
    }
}
