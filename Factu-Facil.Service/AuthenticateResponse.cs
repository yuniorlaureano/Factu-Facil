using FactuFacil.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FactuFacil.Service
{
    public class AuthenticateResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }

        public AuthenticateResponse(User user, string token)
        {
            Id = user.Id;
            Name = user.Name;
            Email = user.Email;
            Token = token;
        }
    }
}
