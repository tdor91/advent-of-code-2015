using System.Text;

const string inputFile = "./input.txt";
var input = await File.ReadAllTextAsync(inputFile);

int result1 = 0;
int result2 = 0;

StringBuilder sequence = new(input);
for (int i = 1; i <= 50; i++)
{
    var segments = GetSegments(sequence.ToString());
    sequence.Clear();

    foreach (var (c, cnt) in segments)
    {
        sequence.Append($"{cnt}{c}");
    }

    if (i == 40) result1 = sequence.Length;
    if (i == 50) result2 = sequence.Length;
}

Console.WriteLine(result1);
Console.WriteLine(result2);

IEnumerable<(char C, int Count)> GetSegments(string s)
{
    var cur = s[0];
    var cnt = 1;

    foreach (var c in s.Skip(1))
    {
        if (c == cur)
        {
            cnt++;
        }
        else
        {
            yield return (cur, cnt);
            cnt = 1;
            cur = c;
        }
    }

    yield return (cur, cnt);
}
