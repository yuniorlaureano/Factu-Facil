using System;
using System.Collections.Generic;
using System.Text;
using FactuFacil.Entity;
using FactuFacil.Repository;

namespace FactuFacil.Repository
{
    public interface IUserRepository : IRepository<User>
    {

    }

    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(FactuFacilContext context) : base(context) 
        {
        
        }
    }
}
