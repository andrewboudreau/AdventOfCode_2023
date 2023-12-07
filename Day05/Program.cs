// https://adventofcode.com/2023/day/5

using System.ComponentModel;
using System.IO.Compression;

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
                (int dest, int src, int length) = iter.Current.Split(' ').Select(int.Parse).ToArray();
                almanac.AddRange(dest, src, length);
            }
        }

        return almanac;
    }

    private void StartMap(string source, string destination)
        => maps.Add(new Map(source, destination));

    private void AddRange(int dest, int src, int length)
    {
        maps[^1].Ranges.Add(new Range(dest, src, length));
    }


    public class Map(string source, string destination)
    {
        public string Source { get; private set; } = source;

        public string Destination { get; private set; } = destination;

        public List<Range> Ranges { get; private set; } = new();

        public int MapToDestination(int source)
        {
            foreach (var range in Ranges)
            {
                if (range.MapToDestination(source, out var destination))
                {
                    return destination;
                }
            }

            return 0;
        }
    }

    public class Range()
    {
        public Range(int dest, int src, int length)
            : this()
        {
            SourceStart = src;
            DestinationStart = dest;
            Length = length;
        }

        public int SourceStart { get; set; }
        public int DestinationStart { get; set; }
        public int Length { get; set; }

        public bool MapToDestination(int source, out int destination)
        {
            destination = 0;
            if (source < SourceStart || source > SourceStart + Length)
            {
                return false;
            }

            destination = (SourceStart - source) + DestinationStart;
            return true;
        }
    }
}
