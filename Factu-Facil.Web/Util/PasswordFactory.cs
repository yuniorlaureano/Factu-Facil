using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BCrypt.Net;

namespace FactuFacil.Web.Util
{
    public static class PasswordFactory
    {
        public static string HashPassword(this string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
    }
}
