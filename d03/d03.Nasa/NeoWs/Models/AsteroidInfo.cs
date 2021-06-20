using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace d03.Nasa.NeoWs.Models
{

    public class AsteroidsNearEarth
    {
        [JsonProperty("near_earth_objects")]
        public Dictionary<DateTime, List<AsteroidInfo>> AsteroidInfos;
    }

    public class MissDistance
    {
        [JsonProperty("kilometers")]
        public string Kilometers = "";
    }
    
    public class CloseApproachData
    {
        [JsonProperty("miss_distance")]
        public MissDistance MissDistance;
    }
    public class AsteroidInfo
    {
        public string Id = "";
        
        [JsonProperty("close_approach_data")]
        public List<CloseApproachData> CloseApproachData;
    }
}