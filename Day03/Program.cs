using Common;

const string inputFile = "./input.txt";
var input = await File.ReadAllTextAsync(inputFile);

HashSet<Point> locations = [new(0, 0)];
var location = new Point(0, 0);
foreach (var c in input)
{
    location += GetStep(c);
    locations.Add(location);
}

var result1 = locations.Count;
Console.WriteLine(result1);

var santaLocation = new Point(0, 0);
var roboSantaLocation = new Point(0, 0);
HashSet<Point> locations2 = [new(0, 0)];
for (int i = 0; i < input.Length; i++)
{
    var c = input[i];
    if (i % 2 == 0)
    {
        santaLocation += GetStep(c);
        locations2.Add(santaLocation);
    }
    else
    {
        roboSantaLocation += GetStep(c);
        locations2.Add(roboSantaLocation);
    }
}
var result2 = locations2.Count;
Console.WriteLine(result2);

Point GetStep(char c) => c switch
{
    '^' => new Point(0, 1),
    'v' => new Point(0, -1),
    '>' => new Point(1, 0),
    '<' => new Point(-1, 0),
    _ => throw new InvalidOperationException()
};