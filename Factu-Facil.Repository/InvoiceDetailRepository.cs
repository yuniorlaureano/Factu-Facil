using System;
using System.Collections.Generic;
using System.Text;
using FactuFacil.Entity;
using FactuFacil.Repository;

namespace FactuFacil.Repository
{
    public interface IInvoiceDetailRepository : IRepository<InvoiceDetail>
    {

    }

    public class InvoiceDetailRepository : RepositoryBase<InvoiceDetail>, IInvoiceDetailRepository
    {
        public InvoiceDetailRepository(FactuFacilContext context) : base(context) 
        {
        
        }
    }
}
