// https://adventofcode.com/2023/day/13

var sections = ReadRecords<Island>(x => new(x)).ToArray();

var index = 0;
var checksum = 0;

foreach (var island in sections)
{
    index++;
    var result = island.FindReflectionAxis();
    if (result.Index == -1)
    {
        island.Render(Console.WriteLine);
        Console.WriteLine();
        continue;
    }

    if (result.IsVertical)
    {
        //Console.WriteLine($"Section:{index} Vertical Reflection at: {result.Index}");
        checksum += result.Index;
        island.OriginalReflection = (result.Index, result.IsVertical);
    }
    else
    {
        //Console.WriteLine($"Section:{index} Horizontal Reflection at: {result.Index}");
        checksum += result.Index;
        island.OriginalReflection = (result.Index, result.IsVertical);
    }
}

Console.WriteLine($"Part 1 Sum: {checksum}");
Console.WriteLine();

// Part 2
index = 0;
checksum = 0;
foreach (var grid in sections)
{
    index++;
    var result = grid.FindAlternativeReflectionAxis();
    if (result.Index == -1)
    {
        grid.Render(Console.WriteLine);
        Console.WriteLine();
        continue;
    }

    if (result.IsVertical)
    {
        Console.WriteLine($"Section:{index} Vertical Reflection at: {result.Index}");
        checksum += result.Index;
    }
    else
    {
        Console.WriteLine($"Section:{index} Horizontal Reflection at: {result.Index}");
        checksum += result.Index;
    }
}
Console.WriteLine($"Part 2 Sum: {checksum}");
Console.WriteLine();

class Island : Grid<char>
{
    public Island(IEnumerable<IEnumerable<char>> map)
        : base(map)
    {
        RowHashes = [];
        ColumnHashes = [];
        OriginalReflection = (-1, false);
        ComputeHashes();
    }
    public (int Index, bool IsVertical) OriginalReflection;

    public int[] RowHashes { get; private set; }

    public int[] ColumnHashes { get; private set; }

    public void ComputeHashes()
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
                return (i * 100);
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

    public (int Index, bool IsVertical) FindReflectionAxis()
    {
        var horizontal = FindHorizontalReflection();
        if (horizontal != -1)
        {
            return (horizontal, false);
        }

        var vertical = FindVerticalReflection();
        if (vertical != -1)
        {
            return (vertical, true);
        }

        return (-1, false);
    }

    public (int Index, bool IsVertical) FindAlternativeReflectionAxis()
    {
        foreach (var node in Nodes())
        {
            var previous = node.Value;
            var fix = previous == '.' ? '#' : '.';

            node.SetValue(fix);
            ComputeHashes();
            var result = FindReflectionAxis();
            node.SetValue(previous);
            if (result.Index != -1 && !(result.IsVertical == OriginalReflection.IsVertical && result.Index == OriginalReflection.Index))
            {
                Console.WriteLine($"Swapped ({node.X},{node.Y}) from '{previous}' to '{fix}'.");
                return result;
            }
        }

        return (-1, false);
    }
}