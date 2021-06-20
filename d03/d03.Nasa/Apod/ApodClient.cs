using System.Threading.Tasks;
using d03.Nasa.Apod.Models;

namespace d03.Nasa.Apod
{
    public class ApodClient: ApiClientBase, INasaClient<int, Task<MediaOfToday[]>>
    {
        public Task<MediaOfToday[]> GetAsync(int count)
        {
            var url = $"https://api.nasa.gov/planetary/apod?api_key={ApiKey}&count={count}";
            return HttpGetAsync<MediaOfToday[]>(url);
        }

        public ApodClient(string apiKey) : base(apiKey)
        {
        }
    }
}