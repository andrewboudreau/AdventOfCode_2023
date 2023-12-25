// https://adventofcode.com/2023/day/8

using Day08;

using static System.Net.Mime.MediaTypeNames;

var input = Read().GetEnumerator();
input.MoveNext();

using EndlessEnumerable<char> path = new(input.Current);
Map map = new();

input.MoveNext();
while (input.MoveNext())
{
    var parts = input.Current!.Split('=', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
    var nodes = parts[1].Trim('(', ')').Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

    map.Add(parts[0], nodes[0], nodes[1]);
}

var starts = map.Nodes.Select(x => x.Key).Where(x => x.EndsWith('A')).ToList();
var currents = [];
var ends = map.Nodes.Select(x => x.Key).Where(x => x.EndsWith('Z')).ToList();

foreach (var start in starts.Order())
{
    Console.WriteLine(start);
}

var next = "";
while ((next = map.Step(current, path.Next())) != finish)
{
    Console.WriteLine($"Stepping from {current} to {next} on step {map.Steps[0]} ");
    current = next;
}


//Part1.Solve(map, path);

class Map()
{
    public Dictionary<int, int> Steps { get; private set; } = [];
    public Dictionary<string, (string Left, string Right)> Nodes = [];

    public void Add(string value, string left, string right)
    {
        Nodes.Add(value, (left, right));
    }

    public string Step(string start, char direction, int travelerId = 0)
    {
        Steps.TryGetValue(travelerId, out var count);
        Steps[travelerId] = count + 1;

        var (Left, Right) = Nodes[start];
        var destination = direction == 'L' ? Left : Right;
        return destination;
    }
}
