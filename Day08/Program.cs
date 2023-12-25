// https://adventofcode.com/2023/day/8


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

// Part1.Solve(map, path);


class Map()
{
    public int Steps { get; private set; }
    public Dictionary<string, (string Left, string Right)> Nodes = [];

    public void Add(string value, string left, string right)
    {
        Nodes.Add(value, (left, right));
    }

    public string Step(string start, char direction)
    {
        Steps++;
        var (Left, Right) = Nodes[start];
        var destination = direction == 'L' ? Left : Right;
        return destination;
    }

}
