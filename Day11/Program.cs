using Common;

const string inputFile = "./input.txt";
var input = await File.ReadAllTextAsync(inputFile);

var pw = new Password(input);

while (!pw.IsValid())
{
    pw.Increment();
}

Console.WriteLine(pw);

pw.Increment();

while (!pw.IsValid())
{
    pw.Increment();
}

Console.WriteLine(pw);

class Password
{
    private char[] chars = [];

    public Password(string pw)
    {
        chars = pw.ToCharArray();
    }

    public bool IsValid()
    {
        return !HasAnyIllegalChar() && HasAscendingSequence() && HasAtLeastTwoPairs();

        bool HasAnyIllegalChar()
        {
            return chars.Any(c => c is 'i' or 'o' or 'l');
        }

        bool HasAscendingSequence()
        {
            return chars.Tripples().Any(t => t.C - t.B == 1 && t.B - t.A == 1);
        }

        bool HasAtLeastTwoPairs()
        {
            int pairs = 0;
            int i = 0;
            while (i < chars.Length - 1 && pairs < 2)
            {
                if (chars[i] == chars[i + 1])
                {
                    pairs++;

                    var nextIndex = Array.FindIndex(chars[i..], c => c != chars[i]);
                    if (nextIndex == -1)
                    {
                        break;
                    }

                    i += nextIndex;
                }
                else
                {
                    i++;
                }
            }

            return pairs >= 2;
        }
    }

    public void Increment()
    {
        for (int i = chars.Length - 1; i >= 0; i--)
        {
            if (!IncChar(ref chars[i]))
            {
                return;
            }
        }

        throw new InvalidOperationException("No more passwords.");
    }

    private bool IncChar(ref char c)
    {
        c++;

        if (c > 'z')
        {
            c = 'a';

            // carry over
            return true;
        }

        return false;
    }

    public override string ToString()
    {
        return new string(chars);
    }
}