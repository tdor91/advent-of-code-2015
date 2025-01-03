const string inputFile = "./input.txt";
var lines = await File.ReadAllLinesAsync(inputFile);

var result1 = lines.Count(IsNice1);
Console.WriteLine(result1);

var result2 = lines.Count(IsNice2);
Console.WriteLine(result2);

bool IsNice1(string str)
{
    string[] invalidPairs = ["ab", "cd", "pq", "xy"];
    char[] vowels = ['a', 'e', 'i', 'o', 'u'];

    bool hasDouble = false;
    int vowelCount = 0;
    for (int i = 0; i < str.Length; i++)
    {
        if (vowels.Contains(str[i]))
        {
            vowelCount++;
        }

        hasDouble |= i < str.Length - 1 && str[i] == str[i + 1];

        if (i < str.Length - 1 && invalidPairs.Contains(str[i..(i + 2)]))
        {
            return false;
        }
    }

    return vowelCount >= 3 && hasDouble;
}

bool IsNice2(string str)
{
    bool satisfiesRule1 = false;
    bool satisfiesRule2 = false;
    for (int i = 0; i < str.Length - 1; i++)
    {
        satisfiesRule1 |= str[(i + 2)..].Contains(str[i..(i + 2)]);

        if (i < str.Length - 2)
        {
            satisfiesRule2 |= str[i] == str[i + 2];
        }
    }

    return satisfiesRule1 && satisfiesRule2;
}