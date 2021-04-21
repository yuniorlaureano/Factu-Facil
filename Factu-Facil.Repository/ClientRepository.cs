using System;
using System.Collections.Generic;
using System.Text;
using FactuFacil.Entity;
using FactuFacil.Repository;

namespace FactuFacil.Repository
{
    public interface IClientRepository : IRepository<Client>
    {

    }

    public class ClientRepository : RepositoryBase<Client>, IClientRepository
    {
        public ClientRepository(FactuFacilContext context) : base(context) 
        {
        
        }
    }
}
