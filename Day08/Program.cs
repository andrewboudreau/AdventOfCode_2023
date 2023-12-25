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

var start = map.Nodes.Select(x => x.Key).Where(x => x.EndsWith('A')).ToList();
var end = map.Nodes.Select(x => x.Key).Where(x => x.EndsWith('Z')).ToHashSet();

var current = start.ToArray();

while (current.Any(x => !end.Contains(x)))
{
    var direction = path.Next();
    for (var i = 0; i < start.Count; i++)
    {
        if (!end.Contains(current[i]))
        {
            current[i] = map.Step(current[i], direction, i);
        }
    }
    if (map.Steps[0] % 10000 == 0)
    {
        Console.WriteLine(map.Steps[0]);
    }
}
foreach(var step in map.Steps)
{
    Console.WriteLine(step);
}
Console.WriteLine(string.Join(", ", map.Steps.Values));
Console.WriteLine($"Ended at {map.Steps[0]} steps");
Console.WriteLine(map.Steps.Values.Aggregate((long)1, (acc, x) => acc * x));

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
