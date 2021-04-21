using System;
using System.Collections.Generic;
using System.Text;
using FactuFacil.Entity;
using FactuFacil.Repository;

namespace FactuFacil.Repository
{
    public interface IInvoiceRepository : IRepository<Invoice>
    {

    }

    public class InvoiceRepository : RepositoryBase<Invoice>, IInvoiceRepository
    {
        public InvoiceRepository(FactuFacilContext context) : base(context) 
        {
        
        }
    }
}
