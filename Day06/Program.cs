using System.Text.RegularExpressions;

const string inputFile = "./input.txt";
var lines = await File.ReadAllLinesAsync(inputFile);

var instructions = lines.Select(ParseInstruction).ToArray();

var lights = new bool[1000 * 1000];
foreach (var instruction in instructions)
{
    for (int x = instruction.From.X; x <= instruction.To.X; x++)
    {
        for (int y = instruction.From.Y; y <= instruction.To.Y; y++)
        {
            var index = IndexOf(x, y);
            lights[index] = instruction.Op(lights[index]);
        }
    }
}
var result1 = lights.Count(b => b);
Console.WriteLine(result1);

var dimmingLights = new int[1000 * 1000];
foreach (var instruction in instructions)
{
    for (int x = instruction.From.X; x <= instruction.To.X; x++)
    {
        for (int y = instruction.From.Y; y <= instruction.To.Y; y++)
        {
            var index = IndexOf(x, y);
            dimmingLights[index] = Math.Max(0, dimmingLights[index] + instruction.Increment);
        }
    }
}
var result2 = dimmingLights.Sum();
Console.WriteLine(result2);

int IndexOf(int x, int y) => x + y * 1000;

(Func<bool, bool> Op, int Increment, (int X, int Y) From, (int X, int Y) To) ParseInstruction(string instr)
{
    const string pattern = @"(.*?) (\d+),(\d+).*?(\d+),(\d+)";
    var match = Regex.Match(instr, pattern);

    Func<bool, bool> op = match.Groups[1].Value switch
    {
        "turn on" => (bool b) => true,
        "turn off" => (bool b) => false,
        "toggle" => (bool b) => !b,
        _ => throw new InvalidOperationException()
    };

    int inc = match.Groups[1].Value switch
    {
        "turn on" => 1,
        "turn off" => -1,
        "toggle" => 2,
        _ => throw new InvalidOperationException()
    };

    var from = (X: int.Parse(match.Groups[2].Value), Y: int.Parse(match.Groups[3].Value));
    var to = (X: int.Parse(match.Groups[4].Value), Y: int.Parse(match.Groups[5].Value));

    return (op, inc, from, to);
}
