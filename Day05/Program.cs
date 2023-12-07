﻿// https://adventofcode.com/2023/day/5

var almanac = ReadTo(Almanac<long>.Parse);
Console.WriteLine(almanac);

var locations = new List<long>();

foreach (var seed in almanac.seeds)
{
    Console.WriteLine("Starting Seed " + seed);
    var dest = almanac.MapToDestination(seed);
    locations.Add(dest);
    Console.WriteLine($"mapping seed {seed} to location {dest}");
    Console.WriteLine("");

    Console.WriteLine("Min location is " + locations.Min());
}

class Almanac<TNumber>(TNumber[] seeds)
    where TNumber : struct, INumber<TNumber>
{
    public TNumber[] seeds = seeds;

    private readonly List<Almanac<TNumber>.Map> maps = [];

    public TNumber MapToDestination(TNumber source)
    {
        TNumber dest = default;
        foreach (var map in maps)
        {
            dest = map.MapToDestination(source);
            source = dest;
        }

        return dest;
    }

    public static Almanac<TNumber> Parse(IEnumerable<string?> data)
    {
        using var iter = data.GetEnumerator();
        iter.MoveNext();

        if (iter.Current is null)
        {
            throw new InvalidOperationException("no data to parse");
        }

        var seeds = iter.Current
            .Split(':')[1]
            .Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
            .Select(x => TNumber.Parse(x, NumberStyles.Integer, default))
            .ToArray();

        var almanac = new Almanac<TNumber>(seeds);
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
            else if (iter.Current.Split(' ', StringSplitOptions.RemoveEmptyEntries).Length != 0)
            {
                var d1 = iter.Current.Split(' ');
                (TNumber dest, TNumber src, TNumber length) = d1
                    .Select(x => TNumber.Parse(x, NumberStyles.Integer, default))
                    .ToArray();

                almanac.AddRange(dest, src, length);
            }
        }

        return almanac;
    }

    private void StartMap(string source, string destination)
        => maps.Add(new Map(source, destination));

    private void AddRange(TNumber dest, TNumber src, TNumber length)
    {
        maps[^1].Ranges.Add(new Range(dest, src, length));
    }

    public class Map(string source, string destination)
    {
        public string Source { get; private set; } = source;

        public string Destination { get; private set; } = destination;

        public List<Range> Ranges { get; private set; } = [];

        public TNumber MapToDestination(TNumber source)
        {
            foreach (var range in Ranges)
            {
                if (range.MapToDestination(source, out var destination))
                {
                    Console.WriteLine($"{Source} -> {Destination} ({source},{destination})");
                    return destination;
                }
            }
            return source;
            //throw new InvalidOperationException($"couldn't find a {Source} -> {Destination} map for source {source}");
        }
    }

    public class Range()
    {
        public Range(TNumber dest, TNumber src, TNumber length)
            : this()
        {
            SourceStart = src;
            DestinationStart = dest;
            Length = length;
        }

        public TNumber SourceStart { get; }
        public TNumber DestinationStart { get; }
        public TNumber Length { get; }

        public bool MapToDestination(TNumber source, out TNumber destination)
        {
            destination = default;
            if (source >= SourceStart && source < SourceStart + Length)
            {
                destination = TNumber.Abs(SourceStart - source) + DestinationStart;
                return true;
            }

            return false;
        }

        public override string ToString()
        {
            return $"src:{SourceStart} dest:{DestinationStart} len:{Length}";
        }
    }

    public override string ToString()
    {
        var output = new StringBuilder();
        foreach (var map in maps)
        {
            output.AppendLine($"{map.Source} -> {map.Destination}, Ranges = {map.Ranges.Count}");
        }
        return output.ToString();
    }
}
