// https://adventofcode.com/2023/day/13


var sections = ReadRecords(lines =>
{
    return new Island(lines);
});

foreach (var grid in sections)
{
    //grid.Render(Console.WriteLine);
    //Console.WriteLine();
}

var index = 0;
var horz = 0;
var vert = 0;
foreach (var grid in sections)
{
    index++;
    var horizontal = grid.FindHorizontalReflection();
    if (horizontal != -1)
    {
        Console.WriteLine($"Section:{index} Horizontal Reflection at: {horizontal}");
        horz += horizontal * 100;
        continue;
    }

    var vertical = grid.FindVerticalReflection();
    if (vertical != -1)
    {
        Console.WriteLine($"Section:{index} Vertical Reflection at: {vertical}");
        vert += vertical;
        continue;
    }

    grid.Render(Console.WriteLine);
    Console.WriteLine();
}

Console.Write($"Sum: {vert + horz}");

public class Island : Grid<char>
{
    public Island(IEnumerable<IEnumerable<char>> map)
        : base(map)
    {
        RowHashes = Rows()
            .Select(row => string.Join("", row.Select(x => x.Value)).GetHashCode())
            .ToArray();

        var columnHashBuilder = new List<int>(Width);
        foreach (var node in Rows().First())
        {
            columnHashBuilder.Add(node.Value + string.Join("", this.DownFrom(node)).GetHashCode());
        }

        ColumnHashes = [.. columnHashBuilder];
    }

    public int[] RowHashes { get; }

    public int[] ColumnHashes { get; }

    public int FindHorizontalReflection()
    {
        for (var i = 1; i < RowHashes.Length; i++)
        {
            var match = true;
            for (var j = 1; j < RowHashes.Length; j++)
            {
                var up = i - j;
                var down = (i - 1) + j;
                if (up >= 0 && down < RowHashes.Length)
                {
                    match = match && RowHashes[up] == RowHashes[down];
                }
                if (!match)
                {
                    break;
                }
            }

            if (match)
            {
                return i;
            }
        }

        return -1;
    }

    public int FindVerticalReflection()
    {
        for (var i = 1; i < ColumnHashes.Length; i++)
        {
            var match = true;
            for (var j = 1; j < ColumnHashes.Length; j++)
            {
                var left = i - j;
                var right = (i - 1) + j;
                if (left >= 0 && right < ColumnHashes.Length)
                {
                    match = match && ColumnHashes[left] == ColumnHashes[right];
                }
                if (!match)
                {
                    break;
                }
            }

            if (match)
            {
                return i;
            }
        }

        return -1;
    }
}