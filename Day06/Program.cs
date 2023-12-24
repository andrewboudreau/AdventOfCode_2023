// https://adventofcode.com/2023/day/6

Console.WriteLine("Day6");

var races = ReadTo(lines =>
{
    var data = lines.ToArray();
    var times = data[0]!.Split(':')[1].Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
    var distances = data[1]!.Split(':')[1].Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

    var races = new List<Race>();
    for (int i = 0; i < times.Length; i++)
    {
        races.Add(
            new Race(
                int.Parse(times[i]), 
                int.Parse(distances[i])));
    }

    return races;
});

Console.WriteLine(races);

public class Race
{
    public int RaceTime { get; private set; }
    public int MaxDistance { get; private set; }
    public List<TimeStep> TimeSteps { get; private set; }
    public int DistanceToBeat { get; }

    public Race(int raceTime, int distanceToBeat)
    {
        RaceTime = raceTime;
        DistanceToBeat = distanceToBeat;
        TimeSteps = [];
    }

    public void AddTimeStep(int time, int speed)
    {
        int distance = time * speed;
        TimeSteps.Add(new TimeStep(time, speed, distance));
        UpdateMaxDistance();
    }

    private void UpdateMaxDistance()
    {
        MaxDistance = 0;
        foreach (var step in TimeSteps)
        {
            MaxDistance += step.Distance;
        }
    }

    public struct TimeStep(int time, int speed, int distance)
    {
        public int Time = time;
        public int Speed = speed;
        public int Distance = distance;
    }
}