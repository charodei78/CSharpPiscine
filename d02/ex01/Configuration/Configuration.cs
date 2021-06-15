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

        public Configuration()
        {
            Data = new Hashtable();
        }
        public void ApplyConfigFile(string path)
        {
            IConfigurationSource? source = null;
            var extention = Path.GetExtension(path);
            
            switch (extention)
            {
                case ".json":
                    source = new JsonSource(path);
                    break;
                case ".yml":
                    source = new YamlSource(path);
                    break;
                default:
                    throw new Exception("Не поддерживаемый формат файла " + path);
            }

            var items = source.ToHash();
            var keys = items.Keys;
            
            foreach (var key in keys)
            {
                Data[key] = items[key];
            }
        }

        public override string  ToString()
        {
            var res = "Configuration" + Environment.NewLine;
            ICollection keys  = Data.Keys;

            foreach (var key in keys)
            {
                res += key + ": " + (string) Data[key]! + Environment.NewLine;
            }

            return res;
        }
    }
}