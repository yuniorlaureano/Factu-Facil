using FactuFacil.Entity;
using FactuFacil.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace FactuFacil.Service
{
    public interface IProductService : IBaseService<Product>
    {
    }

    public class ProductService : BaseService<Product>, IProductService
    {
        public ProductService(IProductRepository repository) : base(repository)
        {
        }
    }
}
