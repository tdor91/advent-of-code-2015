using System.Security.Cryptography;
using System.Text;

const string inputFile = "./input.txt";
var input = await File.ReadAllTextAsync(inputFile);

var result1 = BruteForceSecretEnd(input, hash => hash.StartsWith("00000"));
Console.WriteLine(result1);

var result2 = BruteForceSecretEnd(input, hash => hash.StartsWith("000000"));
Console.WriteLine(result2);

string BruteForceSecretEnd(string secretStart, Func<string, bool> stoppingCondition)
{
    for (int i = 0; ; i++)
    {
        var hash = MD5.HashData(Encoding.UTF8.GetBytes(secretStart + i));
        var hashString = Convert.ToHexString(hash);

        if (stoppingCondition(hashString))
        {
            return i.ToString();
        }
    }
}
