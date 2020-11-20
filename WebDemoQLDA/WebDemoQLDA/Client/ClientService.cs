using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using WebDemoQLDA.IService;

namespace WebDemoQLDA.Client
{
    public class ClientService : IHttpClientService
    {
        private HttpClient _client { get; }
        //private readonly ApiUrlSettings _apiUrls;
        //private readonly ApiServiceInfo _apiServiceInfo;
        //private readonly IHttpContextAccessor _httpContextAccessor;

        public ClientService()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("http://localhost:81/api/");
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            // client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

        }
        public async Task<T> GetAsync<T>(string requestUri)
        {
            if (_client == null)
                return default;

            var response = await _client.GetAsync(requestUri);
            response.EnsureSuccessStatusCode();
            return !response.IsSuccessStatusCode ? default : ParseResponse<T>(await response.Content.ReadAsStringAsync());
        }

        public async Task<T> DeleteAsync<T>(string requestUri)
        {
            if (_client == null)
                return default;

            var response = await _client.DeleteAsync(requestUri);
            response.EnsureSuccessStatusCode();
            return !response.IsSuccessStatusCode ? default : ParseResponse<T>(await response.Content.ReadAsStringAsync());
        }

        public async Task<T> PostAsync<T>(string requestUri, object p)
        {
            if (_client == null)
                return default;

            var serializedContent = JsonConvert.SerializeObject(p);
            var buffer = Encoding.UTF8.GetBytes(serializedContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await _client.PostAsync(requestUri, byteContent);
            response.EnsureSuccessStatusCode();
            return !response.IsSuccessStatusCode ? default : ParseResponse<T>(await response.Content.ReadAsStringAsync());
        }

        public async Task<T> PostAsync<T>(string requestUri, Dictionary<string, string> paramters = null)
        {
            if (_client == null)
                return default;

            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var encodedContent = new FormUrlEncodedContent(paramters);
            var response = await _client.PostAsync(requestUri, encodedContent);
            response.EnsureSuccessStatusCode();
            return !response.IsSuccessStatusCode ? default : ParseResponse<T>(await response.Content.ReadAsStringAsync());
        }

        public async Task<T> PutAsync<T>(string requestUri, object p)
        {
            if (_client == null)
                return default;

            var serializedContent = JsonConvert.SerializeObject(p);
            var buffer = Encoding.UTF8.GetBytes(serializedContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await _client.PutAsync(requestUri, byteContent);
            response.EnsureSuccessStatusCode();
            return !response.IsSuccessStatusCode ? default : ParseResponse<T>(await response.Content.ReadAsStringAsync());
        }
        public async Task<T> PutAsync<T>(string requestUri, Dictionary<string, string> paramters)
        {
            if (_client == null)
                return default;

            var encodedContent = new FormUrlEncodedContent(paramters);
            var response = await _client.PutAsync(requestUri, encodedContent);
            response.EnsureSuccessStatusCode();
            return !response.IsSuccessStatusCode ? default : ParseResponse<T>(await response.Content.ReadAsStringAsync());
        }

        #region Private
        private static T ParseResponse<T>(string content)
        {
            return JsonConvert.DeserializeObject<T>(content);
        }
        #endregion
    }
}
