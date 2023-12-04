// https://adventofcode.com/2023/day/4

Console.WriteLine("Day4");

var cards = Read(factory: line => new ScratchCard(line));
Console.WriteLine($"Part 1: Total score is {cards.Sum(x => x.Score)}.");



class ScratchCard
{
    public ScratchCard(string line)
    {
        var parts = line.Split(':');
        Id = int.Parse(parts[0].Split(' ')[^1]);

        var game = parts[1].Split("|");
        YourNumbers = game[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
        WinningNumbers = game[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
    }

    public int Id { get; }

    public List<int> YourNumbers { get; }

    public List<int> WinningNumbers { get; }

    public int NumberOfWinningNumbers 
        => YourNumbers.Intersect(WinningNumbers).Count();

    public int Score
    {
        get
        {
            if (NumberOfWinningNumbers == 0) return 0;

            var wins = NumberOfWinningNumbers;
            int value = 1;
            while (--wins > 0)
            {
                value *= 2;
            }

            return value;
        }
    }
}