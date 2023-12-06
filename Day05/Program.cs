// https://adventofcode.com/2023/day/5

var almanac = ReadTo<Almanac>(Almanac.Create);
Console.WriteLine(almanac);

class Almanac
{
    private readonly int[] seeds;

    public Almanac(string seeds)
    {
        this.seeds = ParseIntegers(seeds, skipUntil: ':', thenSplitOn: ' ').ToArray();
    }

    public static Almanac Create(IEnumerable<string?> data)
    {
        using var iter = data.GetEnumerator();
        iter.MoveNext();

        var almanac = new Almanac(iter.Current!);
        

        while(iter.MoveNext())
        {

        }
        
        return almanac;
    }

    public class Map
    {
        public string SourceCategory { get; private set; }
    }

}