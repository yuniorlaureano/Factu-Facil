using FactuFacil.Entity;
using FactuFacil.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace FactuFacil.Service
{
    public interface IClientService : IBaseService<Client>
    {
    }

    public class ClientService : BaseService<Client>, IClientService
    {
        public ClientService(IClientRepository repository) : base(repository)
        {
        }
    }
}
