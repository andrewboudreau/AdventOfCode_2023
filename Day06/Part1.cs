namespace Day06;

public class Part1
{
    public static void Solve()
    {
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

        var part1 = 1;
        foreach (var race in races)
        {
            Console.WriteLine("RaceTime: " + race.RaceTime + " Farthest: " + race.DistanceToBeat);
            foreach (var step in race.TimeSteps)
            {
                Console.WriteLine(step);
            }
            Console.WriteLine("Best: " + race.Max);
            Console.WriteLine();
            Console.WriteLine(part1 *= race.NumberOfWinningChargeTimes);
        }
    }


    class Race
    {
        public int RaceTime { get; private set; }
        public List<TimeStep> TimeSteps { get; private set; }
        public int DistanceToBeat { get; }

        public Race(int raceTime, int distanceToBeat)
        {
            RaceTime = raceTime;
            DistanceToBeat = distanceToBeat;
            TimeSteps = [];

            for (var i = 1; i < RaceTime; i++)
            {
                AddTimeStep(RaceTime - i, i);
            }
        }

        private void AddTimeStep(int runTime, int speed)
        {
            int distance = runTime * speed;
            TimeSteps.Add(new TimeStep(runTime, speed, distance, distance > DistanceToBeat));
        }

        public TimeStep Max => TimeSteps.MaxBy(x => x.Distance);

        public int NumberOfWinningChargeTimes => TimeSteps.Count(x => x.Winner);

        public readonly record struct TimeStep(int Time, int Speed, int Distance, bool Winner);
    }
}
