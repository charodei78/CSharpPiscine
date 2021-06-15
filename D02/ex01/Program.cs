using System;
using System.Collections.Generic;
using System.Linq;
using ex01.Configuration;

var config = new Configuration();
var sources = new List<Tuple<string, int>>();


for (int i = 0; i < args.Length; i += 2)
{
    string path = args[i];
    int priority = Int32.Parse(args[i + 1]);
    
    sources.Add(new Tuple<string, int>(path, priority));
}

var sortedSources = sources.OrderBy(t => t.Item2);

foreach (var item in sortedSources)
{
    config.ApplyConfigFile(item.Item1);
}

Console.WriteLine(config);

return 0;



