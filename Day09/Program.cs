using System.Text.RegularExpressions;
using Common;

const string inputFile = "./input.txt";
var lines = await File.ReadAllLinesAsync(inputFile);

var distances = lines.Select(Parse).ToArray();
var cities = distances.SelectMany(d => new[] { d.A, d.B }).Distinct().ToArray();
var routes = GetPermutations(cities, []);

var result1 = routes.Min(order => order.Pairwise().Sum(pair => DistanceOf(pair.A, pair.B, distances)));
Console.WriteLine(result1);

var result2 = routes.Max(order => order.Pairwise().Sum(pair => DistanceOf(pair.A, pair.B, distances)));
Console.WriteLine(result2);


(string A, string B, int Distance) Parse(string line)
{
    const string pattern = @"(.*?) to (.*?) = (\d+)";
    var match = Regex.Match(line, pattern);

    return (A: match.Groups[1].Value, B: match.Groups[2].Value, Distance: int.Parse(match.Groups[3].Value));
}

List<string[]> GetPermutations(string[] remaining, string[] current)
{
    if (remaining.Length == 0)
    {
        return [current];
    }

    List<string[]> permutations = [];
    foreach (var item in remaining)
    {
        var newRemaining = remaining.Where(e => e != item).ToArray();
        var results = GetPermutations(newRemaining, [.. current, item]);
        permutations.AddRange(results);
    }
    return permutations;
}

int DistanceOf(string a, string b, (string A, string B, int Distance)[] distances) =>
    distances.First(d => d.A == a && d.B == b || d.A == b && d.B == a).Distance;
