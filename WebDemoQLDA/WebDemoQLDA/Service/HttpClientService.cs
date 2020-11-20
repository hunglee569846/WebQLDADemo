using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using WebDemoQLDA.IService;

namespace WebDemoQLDA.Service
{
    public class HttpClientService 
    {
        private HttpClient Client { get; }
        //private readonly ApiUrlSettings _apiUrls;
        //private readonly ApiServiceInfo _apiServiceInfo;
        //private readonly IHttpContextAccessor _httpContextAccessor;

        //public Clientaddres(string accessToken)
        //{
        //    Client = new HttpClient();
        //    Client.BaseAddress = new Uri("http://localhost:81/");
        //    Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //    // client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

        //}
        public async Task<T> GetAsync<T>(string requestUri)
        {
            if (Client == null)
                return default;

            var response = await Client.GetAsync(requestUri);
            response.EnsureSuccessStatusCode();
            return !response.IsSuccessStatusCode ? default : ParseResponse<T>(await response.Content.ReadAsStringAsync());
        }

        public async Task<T> DeleteAsync<T>(string requestUri)
        {
            if (Client == null)
                return default;

            var response = await Client.DeleteAsync(requestUri);
            response.EnsureSuccessStatusCode();
            return !response.IsSuccessStatusCode ? default : ParseResponse<T>(await response.Content.ReadAsStringAsync());
        }

        public async Task<T> PostAsync<T>(string requestUri, object p)
        {
            if (Client == null)
                return default;

            var serializedContent = JsonConvert.SerializeObject(p);
            var buffer = Encoding.UTF8.GetBytes(serializedContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await Client.PostAsync(requestUri, byteContent);
            response.EnsureSuccessStatusCode();
            return !response.IsSuccessStatusCode ? default : ParseResponse<T>(await response.Content.ReadAsStringAsync());
        }

        public async Task<T> PostAsync<T>(string requestUri, Dictionary<string, string> paramters = null)
        {
            if (Client == null)
                return default;

            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var encodedContent = new FormUrlEncodedContent(paramters);
            var response = await Client.PostAsync(requestUri, encodedContent);
            response.EnsureSuccessStatusCode();
            return !response.IsSuccessStatusCode ? default : ParseResponse<T>(await response.Content.ReadAsStringAsync());
        }

        public async Task<T> PutAsync<T>(string requestUri, object p)
        {
            if (Client == null)
                return default;

            var serializedContent = JsonConvert.SerializeObject(p);
            var buffer = Encoding.UTF8.GetBytes(serializedContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await Client.PutAsync(requestUri, byteContent);
            response.EnsureSuccessStatusCode();
            return !response.IsSuccessStatusCode ? default : ParseResponse<T>(await response.Content.ReadAsStringAsync());
        }
        public async Task<T> PutAsync<T>(string requestUri, Dictionary<string, string> paramters)
        {
            if (Client == null)
                return default;

            var encodedContent = new FormUrlEncodedContent(paramters);
            var response = await Client.PutAsync(requestUri, encodedContent);
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
