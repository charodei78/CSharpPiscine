using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace d03.Nasa
{
    public abstract class ApiClientBase
    {
        protected string ApiKey { get; }

        protected ApiClientBase(string apiKey)
        {
            ApiKey = apiKey;
        }
        
        protected async Task<T>  HttpGetAsync<T>(string url)
        {
            var client = new HttpClient();
            var response = await client.GetAsync(url);

            var body = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                var message = $"GET \"{url}\" returned {response.StatusCode:d}:\n{body}";
                throw new Exception(message);
            }

            
            return JsonConvert.DeserializeObject<T>(body);
        }
    }
}