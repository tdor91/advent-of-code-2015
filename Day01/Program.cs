const string inputFile = "./input.txt";
var input = await File.ReadAllTextAsync(inputFile);

var opening = input.Count(c => c == '(');
var closing = input.Count(c => c == ')');
var result1 = opening - closing;
Console.WriteLine(result1);

var stack = new Stack<char>(['(']);
var result2 = 1;
foreach (var c in input)
{
    if (c == ')')
    {
        stack.Pop();
        if (stack.Count == 0)
        {
            break;
        }
    }
    else if (c == '(')
    {
        stack.Push(c);
    }

    result2++;
}
Console.WriteLine(result2);
