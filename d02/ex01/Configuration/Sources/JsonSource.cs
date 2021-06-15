using System.Collections;
using System.IO;

namespace ex01.Configuration.Sources
{
    public class JsonSource : IConfigurationSource
    {
        public string Path { get; }

        public JsonSource(string path)
        {
            Path = path;
        }


        public Hashtable ToHash()
        {
            var json = File.ReadAllText(Path);

            var data = System.Text.Json.JsonDocument.Parse(json);
            var items = data.RootElement.EnumerateObject();
            var result = new Hashtable();

            foreach (var item in items)
            {
                result.Add(item.Name, item.Value.ToString());
            }

            return result;
        }
    }
}