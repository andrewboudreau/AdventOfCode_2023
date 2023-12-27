// https://adventofcode.com/2023/day/11

var expansion = 1_000_000 - 1;
var map = new Grid<char>(Read()!);

List<int> expandedColumns = [];
List<int> expandedRows = [];

// Get columns to expand
foreach (var column in map.Rows().First())
{
    if (column.Value == '.' && map.DownFrom(column).All(x => x == '.'))
    {
        expandedColumns.Add(column.X);
    }
}

// Get rows to expand
foreach (var row in map.Rows())
{
    if (row.All(x => x == '.'))
    {
        expandedRows.Add(row.First().Y);
    }
}

//map.Render(Console.WriteLine);
Console.WriteLine($"Expanding Columns {string.Join(", ", expandedColumns)}");
Console.WriteLine($"Expanding Rows {string.Join(", ", expandedRows)}");

var completed = new HashSet<(Node<char>, Node<char>)>();

foreach (var node in map.Nodes())
{
    if (node.Value == '.')
    {
        continue;
    }

    foreach (var otherNode in map.Nodes())
    {
        if (otherNode.Value == '.' || ReferenceEquals(node, otherNode))
        {
            continue;
        }
        if (completed.Contains((node, otherNode)) || completed.Contains((otherNode, node)))
        {
            continue;
        }
        else
        {
            completed.Add((node, otherNode));
        }

        long distance = map.ManhattanDistance(node, otherNode);
        distance += (CountCrossedExpansions(expandedRows, node.Y, otherNode.Y) * expansion);
        distance += (CountCrossedExpansions(expandedColumns, node.X, otherNode.X) * expansion);

        node.SetDistance(node.Distance + distance);
    }
}

Console.WriteLine($"Total Distances {map.Nodes().Sum(x => x.Distance)}");

static int CountCrossedExpansions(List<int> rows, int y1, int y2)
{
    int count = 0;
    int minY = Math.Min(y1, y2);
    int maxY = Math.Max(y1, y2);

    foreach (int row in rows)
    {
        if (row > minY && row < maxY)
        {
            count++;
        }
    }

    return count;
}
