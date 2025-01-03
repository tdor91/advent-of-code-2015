using Common;

const string inputFile = "./input.txt";
var input = await File.ReadAllLinesAsync(inputFile);

var boxes = input
    .Select(line => line.Split("x").ToInts().ToArray())
    .Select(x => (l: x[0], w: x[1], h: x[2]))
    .ToArray();

var result1 = boxes.Sum(CalcWrappingPaper);
Console.WriteLine(result1);

var result2 = boxes.Sum(CalcRibbon);
Console.WriteLine(result2);

int CalcWrappingPaper((int l, int w, int h) box)
{
    int[] surfaces =
    [
        box.l * box.w,
        box.w * box.h,
        box.h * box.l,
    ];

    return surfaces.Sum() * 2 + surfaces.Min();
}

int CalcRibbon((int l, int w, int h) box)
{
    List<int> sides =
    [
        box.l,
        box.w,
        box.h,
    ];
    sides.Sort();

    return sides[0] * 2 + sides[1] * 2  + box.l * box.w * box.h;
}
