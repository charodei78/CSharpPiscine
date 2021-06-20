using System;
using System.Text.Json.Serialization;

namespace d03.Nasa.Apod.Models
{
    public class MediaOfToday
    {
        [JsonPropertyName("date")] public string Date { get; set; } = "";
        
        [JsonPropertyName("title")]
        public string Title {get; set;} = "";
        
        [JsonPropertyName("copyright")]
        public string Copyright {get; set;} = "";
        
        [JsonPropertyName("explanation")]
        public string Explanation {get; set;} = "";
        
        [JsonPropertyName("url")]
        public string Url {get; set;} = "";

        public override string ToString()
        {
            var result = $"{Date}\n" + $"‘{Title}’";
            if (Copyright.Length != 0)
                result += $"by {Copyright};";
            result += $"\n{Explanation}\n{Url}";
            return result;
        }
    }
}