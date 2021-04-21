using FactuFacil.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace FactuFacil.Web.Controllers
{
    public class BaseController : ControllerBase
    {
        protected User GetUser()
        {
            User user = (User)HttpContext.Items["User"];
            return user ?? throw new ArgumentNullException("El usuario no esta logueado");
        }
    }
}
