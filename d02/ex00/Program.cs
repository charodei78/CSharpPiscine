#nullable enable
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using D02.Model;

string jsonString1;
string jsonString2;

try
{
    jsonString1 = File.ReadAllText("./database/movie_reviews.json");
    jsonString2 = File.ReadAllText("./database/book_reviews.json");
}
catch (Exception)
{
    Console.WriteLine("Input files not found");
    return 1;
}

var jsonDocument = System.Text.Json.JsonDocument.Parse(jsonString1);
var movies = jsonDocument.RootElement.GetProperty("results").EnumerateArray();

jsonDocument = System.Text.Json.JsonDocument.Parse(jsonString2);
var books = jsonDocument.RootElement.GetProperty("results").EnumerateArray();
var content = new List<ISearchable>();

content.AddRange(movies.Select(t=>new Movie(t)));
content.AddRange(books.Select(book => new Book(book)));

Console.WriteLine("Input search text:");
string? userInput = Console.ReadLine();

List<ISearchable> results;

if (string.IsNullOrEmpty(userInput))
    userInput = "";

results = content
        .Where(t => t.Title.ToLower().Contains(userInput.ToLower()))
        .OrderBy(t=>t.Title)
        .OrderBy(t=>t.GetType().ToString())
        .ToList();


if (results.Count == 0)
{
    Console.WriteLine($"There are no \"{userInput}\" in media today.");
    return 0;
}

Console.WriteLine($"Items found: {results.Count}\n");

Type? type = null;

foreach (var result in results)
{
    if (type != result.GetType())
    {
        type = result.GetType();
        string typeString = type.ToString().Split(".").Last();
        Console.WriteLine($"\n{typeString} search result [{results.Count(t => t.GetType() == type)}]:");
    }

    Console.WriteLine(result);
}

return 0;