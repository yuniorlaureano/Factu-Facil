using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Essentials;

namespace FactuFacil.Services
{
    public class HttpClientServiceBase<T>
    {
        protected HttpClient client;
        protected string baseEndpoint;
        public bool IsConnected => Connectivity.NetworkAccess == NetworkAccess.Internet;

        public HttpClientServiceBase(string baeUrl, string baseEndpoint, string token)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(baeUrl);
            this.baseEndpoint = baseEndpoint;
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
        }

        public static HttpClientServiceBase<TBase> SetBaseUrl<TBase>(string baeUrl, string baseEndpoint, string token)
        {
            return new HttpClientServiceBase<TBase>(baeUrl, baseEndpoint, token);
        }
        
        public async Task<IEnumerable<T>> GetAsync()
        {
            IEnumerable<T> models = null;

            if (IsConnected)
            {

                var json = await client.GetStringAsync($"api/{baseEndpoint}");
                models = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<T>>(json));
            }

            return models;
        }

        public async Task<T> GetAsync(Guid id)
        {
            if (id != null && IsConnected)
            {
                var json = await client.GetStringAsync($"api/{baseEndpoint}/{id}");
                return await Task.Run(() => JsonConvert.DeserializeObject<T>(json));
            }

            return default(T);
        }

        public async Task<bool> AddAsync(T model)
        {
            if (model == null || !IsConnected)
                return false;

            var serializedModel = JsonConvert.SerializeObject(model);

            var response = await client.PostAsync($"api/{baseEndpoint}", new StringContent(serializedModel, Encoding.UTF8, "application/json"));

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(T model)
        {
            if (model == null || !IsConnected)
                return false;

            var serializedModel = JsonConvert.SerializeObject(model);
            var response = await client.PutAsync($"api/{baseEndpoint}", new StringContent(serializedModel, Encoding.UTF8 ,"application/json"));

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            if (id == null || !IsConnected)
                return false;

            var response = await client.DeleteAsync($"api/{baseEndpoint}/{id}");

            return response.IsSuccessStatusCode;
        }
    }
}
