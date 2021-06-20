using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using d03.Nasa.Apod;
using d03.Nasa.NeoWs;
using d03.Nasa.NeoWs.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace day03
{
    class Program
    {
        private static IConfigurationRoot _configuration;
        
        public static void Main(string[] args)
        {
            Console.WriteLine(Environment.CurrentDirectory);
            _configuration = new ConfigurationBuilder()
                .AddJsonFile(Environment.CurrentDirectory+"/appsettings.json").Build();
            MainAsync(args).GetAwaiter().GetResult();
        }

        private static async Task MainAsync(string[] args)
        {
            try
            {
                // Раскомментировать для ex00
                // var client = new ApodClient(_configuration["ApiKey"]);
                // var results = await client.GetAsync(Int32.Parse(args[0]));
                //
                // foreach (var result in results)
                // {
                //     Console.WriteLine(result);
                //     Console.WriteLine();
                // }
                var client = new NeoWsClient(_configuration["ApiKey"]);
                
                var startDateString = _configuration.GetSection("NeoWs:StartDate").Value;
                var endDateString = _configuration.GetSection("NeoWs:EndDate").Value;
                
                var inpit = new AsteroidRequest(startDateString, endDateString, args[0]);
                var results = await client.GetAsync(inpit);
                
                foreach (var asteroidLookup in results)
                {
                    Console.WriteLine(asteroidLookup);
                    Console.WriteLine();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
   
}