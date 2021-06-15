using System;
using System.Collections.Generic;
using System.Linq;
using ex01.Configuration;


            
    var config = new Configuration();
    var sources = new List<Tuple<string, int>>();


    for (int i = 0; i < args.Length; i += 2)
    {
        string path = args[i];
        int priority = 0;
        if (!Int32.TryParse(args[i + 1], out priority))
        {
            Console.WriteLine("Invalid data. Check your input and try again.");
            return 1;
        }

        sources.Add(new Tuple<string, int>(path, priority));
    }

    var sortedSources = sources.OrderBy(t => t.Item2);

    foreach (var item in sortedSources)
    {
        try
        {
            config.ApplyConfigFile(item.Item1);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            Console.WriteLine("Invalid data. Check your input and try again.");
            return 1;
        }
    }

    Console.WriteLine(config);

    return 1;


