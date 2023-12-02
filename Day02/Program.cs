// https://adventofcode.com/2023/day/2

var games = Read(factory: Game.GameFactory);

games.ToConsole(x => string.Join("\r\n", x));

enum Colors { red, blue, green }

class Game(int gameId)
{
    public readonly int GameId = gameId;
    public List<List<(int Number, Colors color)>> Sets = [];

    public void AddSet() => Sets.Add([]);

    public void AddGrab(int number, Colors color)
        => Sets.Last().Add((number, color));

    public override string ToString()
    {
        var sets = "";
        foreach (var set in Sets)
        {
            sets += string.Join(' ', set.Select(x => $"{x.Number} {x.color}")) + "; ";
        }
        return $"Game {GameId}: {sets}";
    }

    public static Game GameFactory(string record)
    {
        var parts = record.Split(':');
        var gameId = int.Parse(parts[0].Split(' ')[1]);

        var game = new Game(gameId);

        var sets = parts[1].Split(';');
        foreach (var set in sets)
        {
            game.AddSet();
            foreach (var blocks in set.Split(','))
            {
                var block = blocks.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                game.AddGrab(
                    number: int.Parse(block[0]), 
                    color: (Colors)Enum.Parse(typeof(Colors), block[1]));
            }
        }

        return game;
    }
}
