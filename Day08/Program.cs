const string inputFile = "./input.txt";
var lines = await File.ReadAllLinesAsync(inputFile);

var result1 = lines.Sum(line => line.Length - Unencode(line).Length);
Console.WriteLine(result1);

var result2 = lines.Sum(line => Encode(line).Length - line.Length);
Console.WriteLine(result2);

string Unencode(string str)
{
    string result = str;

    result = result[1..^1];
    result = result.Replace(@"\\", @"#").Replace(@"\""", @"#");

    while (result.Contains(@"\x"))
    {
        var index = result.IndexOf(@"\x");
        result = result.Remove(index, 4).Insert(index, "#");
    }

    return result;
}

string Encode(string str)
{
    string result = str;

    result = result.Replace(@"""", @"##").Replace(@"\", @"##");
    result = @"""" + result + @"""";

    return result;
}