// https://adventofcode.com/2023/day/4

Console.WriteLine("Day4");

var cards = Read(factory: line => new ScratchCard(line));
Console.WriteLine($"Part 1: Total score is {cards.Sum(x => x.Score)}.");

var games = cards.Select(card => new Game(card)).ToList();

var index = 0;
do
{
    while (games[index].Unplayed > 0)
    {
        var winnings = games[index].Play();
        for (var n = 1; n < games.Count && n <= winnings; n++)
        {
            games[index + n].Unplayed++;
        }
    }

    index = Math.Min(index + 1, games.Count - 1);

} while (games[index].Unplayed != 0);

Console.WriteLine($"There are {games.Sum(g => g.Played)} total cards.");

class Game(ScratchCard card)
{
    public ScratchCard Card { get; private set; } = card;
    public int Unplayed { get; set; } = 1;
    public int Played { get; private set; }
    public int Play()
    {
        Unplayed--;
        Played++;
        return Card.NumberOfWinningNumbers;
    }
}

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