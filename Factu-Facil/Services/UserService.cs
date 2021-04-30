using FactuFacil.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace FactuFacil.Services
{
    public class UserService : HttpClientServiceBase<UserViewModel>
    {
        public UserService(string token) : base(App.BackendUrl, "user", token)
        { }

        public async Task<AuthViewModel> Authenticate(UserViewModel model)
        {
            if (model == null || !base.IsConnected)
                throw new ArgumentException("Debe proveer usuario y contraseña");

            string serializedModel = JsonConvert.SerializeObject(model);

            HttpResponseMessage response = await client.PostAsync($"api/{base.baseEndpoint}/auth", new StringContent(serializedModel, Encoding.UTF8, "application/json"));
            
            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException("Error en el servidor");

            string stringJson = await response.Content.ReadAsStringAsync();
            AuthViewModel user = JsonConvert.DeserializeObject<AuthViewModel>(stringJson);

            if (string.IsNullOrWhiteSpace(user?.Token))
                throw new HttpRequestException("No se pudo logear, intente nuevamente");

            return user;
        }
    }
}
