// https://adventofcode.com/2023/day/2

var games = Read(factory: Game.Factory);

var checkSum = 0;
var min = (Red: 12, Green: 13, Blue: 14);

foreach (var game in games)
{
    var (Red, Green, Blue) = game.MaxHolding();
    if (Red <= min.Red && Green <= min.Green && Blue <= min.Blue)
    {
        checkSum += game.GameId;
    }
}

Console.WriteLine($"Check sum: {checkSum}");

class Game(int gameId)
{
    public enum Colors { Red, Blue, Green }

    public readonly int GameId = gameId;
    public List<List<(int Number, Colors color)>> Sets = [];

    public (int Red, int Green, int Blue) MaxHolding()
    {
        var red = Sets.Select(x => x.FirstOrDefault(y => y.color == Colors.Red).Number).Max();
        var green = Sets.Select(x => x.FirstOrDefault(y => y.color == Colors.Green).Number).Max();
        var blue = Sets.Select(x => x.FirstOrDefault(y => y.color == Colors.Blue).Number).Max();
        return (red, green, blue);
    }

    public override string ToString()
    {
        var sets = "";
        foreach (var set in Sets)
        {
            sets += string.Join(' ', set.Select(x => $"{x.Number} {x.color}")) + "; ";
        }
        return $"Game {GameId}: {sets}";
    }

    private void AddGrab(int number, Colors color)
        => Sets.Last().Add((number, color));

    public static Game Factory(string record)
    {
        var parts = record.Split(':');
        var gameId = int.Parse(parts[0].Split(' ')[1]);

        var game = new Game(gameId);

        var sets = parts[1].Split(';');
        foreach (var set in sets)
        {
            game.Sets.Add([]);
            foreach (var blocks in set.Split(','))
            {
                var block = blocks.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                game.AddGrab(
                    number: int.Parse(block[0]),
                    color: (Colors)Enum.Parse(typeof(Colors), block[1], ignoreCase: true));
            }
        }

        return game;
    }
}
