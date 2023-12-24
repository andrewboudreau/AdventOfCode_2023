// https://adventofcode.com/2023/day/6

Part1.Solve();

var data = Read().ToArray();
var raceTime = ulong.Parse(data[0]!.Split(':')[1].Replace(" ", ""));
var distanceToBeat = ulong.Parse(data[1]!.Split(':')[1].Replace(" ", ""));
var waysToWin = 0;

for (ulong i = 1; i < raceTime; i++)
{
    if ((raceTime - i) * i > distanceToBeat)
    {
        waysToWin++;
    }
}

Console.WriteLine(waysToWin);
