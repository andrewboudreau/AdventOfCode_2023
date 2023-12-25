// https://adventofcode.com/2023/day/9

using System.Threading.Channels;

var part1_Samples = Read(row => row.Split(' ').Select(int.Parse).ToArray()).ToList();
var part2_Samples = Read(row => row.Split(' ').Select(int.Parse).Reverse().ToArray()).ToList();

var checksum = 0;
foreach (var sample in part2_Samples)
{
    List<List<int>> slopes = new();
    slopes.Add([.. sample]);

    do
    {
        slopes.Add(
            [.. Differences(slopes[^1])]);

    } while (slopes[^1].Any(x => x != 0));

    var extras = new List<int>() { 0 };
    for (var i = slopes.Count - 1; i > 0; i--)
    {
        var extraIndex = (slopes.Count - 1) - i;
        var prev = slopes[i - 1][^1];
        var nextra = extras[extraIndex];
        var next = prev + nextra;
        extras.Add(next);
    }
    checksum += extras[^1];
    extras.ToConsole();
}
checksum.ToConsole("CheckSum is");


static IEnumerable<int> Differences(IEnumerable<int> source)
{
    if (source == null)
        throw new ArgumentNullException(nameof(source));

    using (var enumerator = source.GetEnumerator())
    {
        if (!enumerator.MoveNext())
            yield break;

        int previous = enumerator.Current;
        while (enumerator.MoveNext())
        {
            yield return enumerator.Current - previous;
            previous = enumerator.Current;
        }
    }
}

