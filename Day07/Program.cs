using System.Text.RegularExpressions;

const string inputFile = "./input.txt";
var lines = await File.ReadAllLinesAsync(inputFile);

var gates = lines.ToDictionary(line => line.Split(" -> ")[1], line => ParseLhs(line.Split(" -> ")[0]));

var resolvedWires = ResolveWires(gates);
var result1 = resolvedWires["a"];
Console.WriteLine(result1);

var gates2 = gates.ToDictionary();
gates2["b"] = new($"{resolvedWires["a"]}", "VALUE");

var resolvedWires2 = ResolveWires(gates2);
var result2 = resolvedWires2["a"];
Console.WriteLine(result2);

Dictionary<string, ushort> ResolveWires(Dictionary<string, GateInput> gates)
{
    var resolvedWires = new Dictionary<string, ushort>();

    while (resolvedWires.Count != gates.Count)
    {
        foreach (var (wire, gateInput) in gates.Where(kvp => !resolvedWires.ContainsKey(kvp.Key)))
        {
            if (TryGetValue(gateInput.Left, resolvedWires, out var leftValue))
            {
                if (gateInput.Op is "=" or "VALUE")
                {
                    resolvedWires[wire] = leftValue;
                }
                else if (gateInput.Op == "NOT")
                {
                    resolvedWires[wire] = (ushort)~leftValue;
                }
                else if (TryGetValue(gateInput.Right, resolvedWires, out var rightValue))
                {
                    resolvedWires[wire] = gateInput.Op switch
                    {
                        "AND" => (ushort)(leftValue & rightValue),
                        "OR" => (ushort)(leftValue | rightValue),
                        "LSHIFT" => (ushort)(leftValue << rightValue),
                        "RSHIFT" => (ushort)(leftValue >> rightValue),
                        _ => throw new InvalidOperationException()
                    };
                }
            }
        }
    }

    return resolvedWires;

    bool TryGetValue(string s, Dictionary<string, ushort> resolvedWires, out ushort value) =>
        ushort.TryParse(s, out value) || resolvedWires.TryGetValue(s, out value);
}

GateInput ParseLhs(string lhs)
{
    if (ushort.TryParse(lhs, out ushort value))
    {
        return new($"{value}", Op: "VALUE");
    }

    if (!lhs.Contains(" "))
    {
        return new(lhs, Op: "=");
    }

    const string pattern =
        @"(^(?<left>[^ ]+) (?<op>(AND|OR|LSHIFT|RSHIFT)) (?<right>[^ ]+)$)|(^(?<op>NOT) (?<left>[^ ]+)(?<right>))";
    var match = Regex.Match(lhs, pattern, RegexOptions.Multiline);

    return new(match.Groups["left"].Value, match.Groups["right"].Value, match.Groups["op"].Value);
}

record GateInput(string Left, string Right, string Op)
{
    public GateInput(string Left, string Op) : this(Left, "", Op)
    {
        if (Op != "NOT" && Op != "VALUE" && Op != "=")
        {
            throw new InvalidOperationException();
        }
    }
}