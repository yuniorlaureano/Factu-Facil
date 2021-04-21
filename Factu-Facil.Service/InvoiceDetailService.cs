using FactuFacil.Entity;
using FactuFacil.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace FactuFacil.Service
{
    public interface IInvoiceDetailService : IBaseService<InvoiceDetail>
    {
    }

    public class InvoiceDetailService : BaseService<InvoiceDetail>, IInvoiceDetailService
    {
        public InvoiceDetailService(IInvoiceDetailRepository repository) : base(repository)
        {
        }
    }
}
