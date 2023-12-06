// https://adventofcode.com/2023/day/5

var almanac = ReadTo(Almanac.Create);
Console.WriteLine(almanac);

class Almanac
{
    private readonly int[] seeds;
    private readonly List<Map> maps;
    private readonly List<int> ranges;

    public Almanac(string seeds)
    {
        this.seeds = ParseIntegers(seeds, skipUntil: ':', thenSplitOn: ' ').ToArray();
        maps = [];
        ranges = [];
    }

    private void StartMap(string source, string destination)
        => maps.Add(new Map(source, destination));

    private void AddRange(IEnumerable<int> ranges)
        => this.ranges.AddRange(ranges);

    public static Almanac Create(IEnumerable<string?> data)
    {
        using var iter = data.GetEnumerator();
        iter.MoveNext();

        var almanac = new Almanac(iter.Current!);
        while (iter.MoveNext())
        {
            if (iter.Current is null)
            {
                break;
            }
            else if (iter.Current.Contains(':'))
            {
                var part = iter.Current.Split(' ')[0].Split('-');
                almanac.StartMap(part[0], part[^1]);
            }
            else if (iter.Current.Split(' ').Length != 0)
            {
                almanac.AddRange(iter.Current.Split(' ').Select(int.Parse));
            }
        }

        return almanac;
    }

    public class Map(string source, string destination)
    {
        public string Source { get; private set; } = source;

        public string Destination { get; private set; } = destination;

        public List<int> Ranges { get; private set; } = new();
    }
}