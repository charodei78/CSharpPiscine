#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using d03.Nasa.Apod.Models;
using d03.Nasa.NeoWs.Models;

namespace d03.Nasa.NeoWs
{
    public class NeoWsClient: ApiClientBase, INasaClient<AsteroidRequest, Task<AsteroidLookup[]>>
    {
        public async Task<AsteroidLookup[]> GetAsync(AsteroidRequest input)
        {
            var url = "https://api.nasa.gov/neo/rest/v1/feed?api_key="
                      + ApiKey
                      + "&start_date=" + input.StartDate.ToString("yyyy-MM-dd")
                      + "&end_date=" + input.StartDate.ToString("yyyy-MM-dd");
            AsteroidsNearEarth root = await HttpGetAsync<AsteroidsNearEarth>(url);
            
            List<AsteroidInfo> info = new();

            foreach (var infosValue in root.AsteroidInfos.Values)
            {
                info.AddRange(infosValue);
            }
            
            var ids = info
                .OrderBy(t => t.CloseApproachData[0].MissDistance.Kilometers
                )
                .Take(input.ResultCount)
                .Select(t=>t.Id);

            var lookupList = new List<AsteroidLookup>();
            var lookupListTasks = new List<Task<AsteroidLookup>>();
            
            foreach (var id in ids)
            {
                url = $"https://api.nasa.gov/neo/rest/v1/neo/{id}?api_key={ApiKey}";
                lookupListTasks.Add(HttpGetAsync<AsteroidLookup>(url));
            }

            Task allTasks = Task.WhenAll(lookupListTasks);
            allTasks.Wait();

            if (allTasks.IsCompleted)
            {
                foreach (var task in lookupListTasks)
                {
                    lookupList.Add(task.Result);
                }
                return lookupList.ToArray();
            }
            else
            {
                throw new Exception("Произошла ошибка запроса на сервер");
            }
        }

        public NeoWsClient(string apiKey) : base(apiKey)
        {
        }
    }
}