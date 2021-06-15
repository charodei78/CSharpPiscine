using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using ex01.Configuration.Sources;

namespace ex01.Configuration
{
    public class Configuration
    {
        public Hashtable Data { get; }

        public void ApplyConfigFile(string path)
        {
            IConfigurationSource source;
            var extention = Path.GetExtension(path);
            
            switch (extention)
            {
                case "json":
                    source = new JsonSource(path);
                    break;
                case "yaml":
                    source = new YamlSource(path);
                    break;
                default:
                    Console.WriteLine("Не поддерживаемый формат файла " + path);
                    return;
            }

            var items = source.ToHash();
            var keys = items.Keys;
            
            foreach (var key in keys)
            {
                Data[key] = items[key];
            }
        }

        public string ToString()
        {
            var res = "Configuration" + Environment.NewLine;
            var keys = Data.Keys;
            
            foreach (var key in keys)
            {
                res += key + ": " + (string) Data[key] + Environment.NewLine;
            }

            return res;
        }
    }
}