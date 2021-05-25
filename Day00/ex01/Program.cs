using System;
using System.IO;
using System.Text.RegularExpressions;

string[] names = File.ReadAllLines("./us.txt");



static int CmpWord(string needle, string haystack, int limit)
{
    var distacnes = new int[3];
    
    if (limit < 0)
        return 1;
    
    if (limit < 0 || (needle == "" && haystack == "")) return 0;
    if (needle.Length == 0 && haystack.Length != 0) return 1 + CmpWord(needle, haystack[1..], limit - 1);
    if (needle.Length != 0 && haystack.Length == 0) return 1 + CmpWord(needle[1..], haystack, limit - 1);
    if (needle[0] == haystack[0]) return CmpWord(needle[1..], haystack[1..], limit);

    
    // try change
    limit -= 1;
    distacnes[0] = 1 + CmpWord(needle[1..], haystack[1..], limit);
    if (distacnes[0] == 0)
        return 0;
    distacnes[1] = 1 + CmpWord(needle, haystack[1..], limit);
    if (distacnes[1] == 0)
        return 0;
    distacnes[2] = 1 + CmpWord(needle[1..], haystack, limit);
    if (distacnes[2] == 0)
        return 0;
    return Math.Min(Math.Min(distacnes[0], distacnes[1]), distacnes[2]);
}

static string[] GetSimilar(string needle, string[] haystack)
{
    int arraySize = 10, resultIndex = 0;
    
    string[] foundString = new string[arraySize];
    int[] foundCmp = new int[arraySize];

    foreach ( var word in haystack)
    {
        int result = CmpWord(needle.ToLower(), word.ToLower(), 2);
        if (result <= 2)
        {
            if (resultIndex >= arraySize)
            {
                arraySize *= 2;
                Array.Resize<int>(ref foundCmp, arraySize);
                Array.Resize<string>(ref foundString, arraySize);
            }
            foundString[resultIndex] = word;
            foundCmp[resultIndex] = result;
            resultIndex++;
        }
    }
    Array.Resize<string>(ref foundString, resultIndex);
    Array.Resize<int>(ref foundCmp, resultIndex);

    Array.Sort(foundCmp, foundString);
    
    return foundString;
}


static bool CheckName(string name)
{
    if (String.IsNullOrEmpty(name))
        return false;
    var rx = new Regex(@"^[a-zA-Z]+[\s | -]{0,1}[a-zA-Z]+$");
    if (rx.IsMatch(name))
        return true;
    return false;
}

static int Invalid()
{
    Console.Write("Invalid name");
    return 1;
}

static bool DidYouMean(string name)
{
    Console.Write($"Did you mean “{name}”? Y/N: ");
    var key = Console.ReadKey();
    Console.WriteLine();
    if (key.Key.ToString() != "Y")
        return false;
    PrintName(name);
    return true;
}

static void PrintName(string name)
{
    Console.WriteLine($"Hello, {name}!");
}

static int NotFound()
{
    Console.WriteLine("Your name was not found.");
    return 1;
}

Console.Write("Enter name: ");
string inputName = Console.ReadLine();


if (String.IsNullOrEmpty(inputName))
    return Invalid();

inputName = inputName.Trim();

if (!CheckName(inputName))
    return Invalid();

inputName = Char.ToUpper(inputName[0]) + inputName.Substring(1).ToLower();

if (Array.IndexOf(names, inputName) != -1)
{
    PrintName(inputName);
    return 0;
}

string[] namesFound = GetSimilar(inputName, names);

foreach (var variant in namesFound)
{
    if (DidYouMean(variant))
        return 0;
}

NotFound();

return 0;