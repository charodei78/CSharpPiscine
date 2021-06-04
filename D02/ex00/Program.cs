#nullable enable
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using D02.Model;

string jsonString = File.ReadAllText("./database/movie_reviews.json");
var jsonDocument = System.Text.Json.JsonDocument.Parse(jsonString);
var movies = jsonDocument.RootElement.GetProperty("results").EnumerateArray();
jsonString = File.ReadAllText("./database/book_reviews.json");

jsonDocument = System.Text.Json.JsonDocument.Parse(jsonString);
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