using FactuFacil.Entity;
using FactuFacil.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace FactuFacil.Service
{
    public interface IInvoiceService : IBaseService<Invoice>
    {
    }

    public class InvoiceService : BaseService<Invoice>, IInvoiceService
    {
        public InvoiceService(IInvoiceRepository repository) : base(repository)
        {
        }
    }
}
