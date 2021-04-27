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
        private HttpClient client;
        private string baseEndpoint;
        bool IsConnected => Connectivity.NetworkAccess == NetworkAccess.Internet;

        public HttpClientServiceBase(string baeUrl, string baseEndpoint)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(baeUrl); //http://10.0.2.2:5000
            this.baseEndpoint = baseEndpoint;
        }

        public static HttpClientServiceBase<TBase> SetBaseUrl<TBase>(string baeUrl, string baseEndpoint)
        {
            return new HttpClientServiceBase<TBase>(baeUrl, baseEndpoint);
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
            var buffer = Encoding.UTF8.GetBytes(serializedModel);
            var byteContent = new ByteArrayContent(buffer);

            var response = await client.PutAsync(new Uri($"api/{baseEndpoint}"), byteContent);

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
