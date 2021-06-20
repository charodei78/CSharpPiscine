using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace d03.Nasa.NeoWs.Models
{
    public class AsteroidLookup
    {
        public override string ToString()
        {
            var result =
                $"- Asteroid {Name}, SPK-ID: {Id}\n" +
                "IS POTENTIALLY HAZARDOUS!\n" +
                $"Classification: {OrbitalData.OrbitClass.OrbitClassType}" +
                $", {OrbitalData.OrbitClass.OrbitClassDescription}.\n" +
                $"Url: {NasaUrl}.";
            return result;
        }

        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("nasa_jpl_url")]
        public string NasaUrl;
        [JsonProperty("orbital_data")]
        public OrbitalData OrbitalData;
        
    }

    public class OrbitalData
    {
        [JsonProperty("orbit_class")]
        public OrbitClass OrbitClass = new OrbitClass();
    }

    public class OrbitClass
    {
        [JsonProperty("orbit_class_type")]
        public string OrbitClassType = "";
        [JsonProperty("orbit_class_description")]
        public string OrbitClassDescription = "";
    }
}