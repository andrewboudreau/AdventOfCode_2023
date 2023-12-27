using System.Text;

// https://adventofcode.com/2023/day/10

Pipes pipes = new(Read()!);
pipes.Each(node =>
{
    if (node.Value == 'S')
    {
        node.Visit();

        pipes.player = node;
        pipes.start = node;
        if (args.First().Contains("sample"))
        {
            node.SetValue('F');
        }
        else
        {
            // I looked
            node.SetValue('|');
        }
    }
});

while (!ReferenceEquals(pipes.MoveForward(), pipes.start))
{
    Console.WriteLine(pipes);
}

class Pipes : Grid<char>
{
    public Node<char> player;
    public Node<char> start;
    public int steps = 0;

    public Pipes(IEnumerable<IEnumerable<char>> map)
        : base(map)
    {
        player = default!;
        start = default!;
    }

    public Node<char> MoveForward()
    {
        steps++;
        switch (player.Value)
        {
            case '|':
                if (!(this[player.X, player.Y - 1]?.IsVisited ?? true))
                {
                    player = this[player.X, player.Y - 1]
                        ?? throw new InvalidOperationException($"Attempted to set player to null position at {player.X}, {player.Y - 1}");
                }
                else
                {
                    player = this[player.X, player.Y + 1]
                        ?? throw new InvalidOperationException($"Attempted to set player to null position at {player.X}, {player.Y + 1}");
                }
                break;
            case '-':
                if (!(this[player.X - 1, player.Y]?.IsVisited ?? true))
                {
                    player = this[player.X - 1, player.Y]
                        ?? throw new InvalidOperationException($"Attempted to set player to null position at {player.X - 1}, {player.Y}");
                }
                else
                {
                    player = this[player.X + 1, player.Y]
                        ?? throw new InvalidOperationException($"Attempted to set player to null position at {player.X + 1}, {player.Y}");
                }
                break;
            case 'L':
                if (!(this[player.X + 1, player.Y]?.IsVisited ?? true))
                {
                    player = this[player.X + 1, player.Y]
                        ?? throw new InvalidOperationException($"Attempted to set player to null position at {player.X + 1}, {player.Y}");
                }
                else
                {
                    player = this[player.X, player.Y - 1]
                        ?? throw new InvalidOperationException($"Attempted to set player to null position at {player.X + 1}, {player.Y - 1}");
                }
                break;
            case 'J':
                if (!(this[player.X - 1, player.Y]?.IsVisited ?? true))
                {
                    player = this[player.X - 1, player.Y]
                        ?? throw new InvalidOperationException($"Attempted to set player to null position at {player.X - 1}, {player.Y}");
                }
                else
                {
                    player = this[player.X, player.Y - 1]
                        ?? throw new InvalidOperationException($"Attempted to set player to null position at {player.X}, {player.Y - 1}");
                }
                break;
            case '7':
                if (!(this[player.X - 1, player.Y]?.IsVisited ?? true))
                {
                    player = this[player.X - 1, player.Y]
                        ?? throw new InvalidOperationException($"Attempted to set player to null position at {player.X - 1}, {player.Y}");
                }
                else
                {
                    player = this[player.X, player.Y + 1]
                        ?? throw new InvalidOperationException($"Attempted to set player to null position at {player.X}, {player.Y + 1}");
                }
                break;
            case 'F':
                if (!(this[player.X + 1, player.Y]?.IsVisited ?? true))
                {
                    player = this[player.X + 1, player.Y]
                        ?? throw new InvalidOperationException($"Attempted to set player to null position at {player.X + 1}, {player.Y}");
                }
                else
                {
                    player = this[player.X, player.Y - 1]
                        ?? throw new InvalidOperationException($"Attempted to set player to null position at {player.X}, {player.Y - 1}");
                }
                break;
            default:
                throw new InvalidOperationException($"Invalid value '{player.Value}' at {player.X},{player.Y}");
        }

        player.Visit();
        return player;
    }

    public override string ToString()
    {
        StringBuilder sb = new();
        var lineCount = 0;
        foreach (var line in Rows())
        {
            foreach (var chr in line)
            {
                if (chr == '.')
                    sb.Append(chr);
                else if (ReferenceEquals(chr, player))
                    sb.Append('S');
                else if (chr == '|')
                    sb.Append('│');
                else if (chr == '-')
                    sb.Append('─');
                else if (chr == 'L')
                    sb.Append('└');
                else if (chr == 'J')
                    sb.Append('┘');
                else if (chr == '7')
                    sb.Append('┐');
                else if (chr == 'F')
                    sb.Append('┌');
                else
                    sb.Append(chr);
            }

            lineCount++;
            if (lineCount != Width)
            {
                sb.AppendLine();
            }
        }
        return sb.ToString();
    }
}

/*
 The pipes are arranged in a two-dimensional grid of tiles:

| is a vertical pipe connecting north and south. │  ║
- is a horizontal pipe connecting east and west. ─  ═
L is a 90-degree bend connecting north and east. └  ╚
J is a 90-degree bend connecting north and west. ┘  ╝
7 is a 90-degree bend connecting south and west. ┐  ╗
F is a 90-degree bend connecting south and east. ┌  ╔
. is ground; there is no pipe in this tile.
S is the starting position of the animal; there is a pipe on this tile, but your sketch doesn't show what shape the pipe has.

 */

