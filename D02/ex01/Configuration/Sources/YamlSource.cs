using System.Collections;
using System.IO;
using System.Text.Json;
using YamlDotNet;
using YamlDotNet.Serialization;

namespace ex01.Configuration.Sources
{
    public class YamlSource : IConfigurationSource
    {
        public string Path { get; }
        public YamlSource(string path)
        {
            Path = path;
        }
        
        public Hashtable ToHash()
        {
            var deserializer = new DeserializerBuilder().Build();
            var yaml = File.OpenText(Path);
            
            var data = deserializer.Deserialize<JsonElement>(yaml);
            var items = data.EnumerateObject();
            
            var result = new Hashtable();

            foreach (var item in items)
            {
                result.Add(item.Name, item.Value);
            }

            return result;
        }
    }
}