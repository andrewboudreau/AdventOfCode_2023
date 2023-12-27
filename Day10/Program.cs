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
        if (args.First().Contains("sample4"))
        {
            node.SetValue('7');
        }
        else if (args.First().Contains("sample"))
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

//Console.WriteLine(pipes.ToString((pipes.player.X, pipes.player.Y, 20)));

while (!ReferenceEquals(pipes.MoveForward(), pipes.start))
{
    //Console.Clear();
    //Console.WriteLine(pipes.ToString((pipes.player.X, pipes.player.Y, 20)));
    //Thread.Sleep(1);
}

Console.WriteLine(pipes.steps);
Console.WriteLine(pipes.steps / 2);

var enclosed = 0;

// iterate over each row and count the number of vertical lines crossed, even number inside, odd number outside.
foreach (var row in pipes.Rows().Skip(1).Take(pipes.Height - 2))
{
    var crosses = 0;
    var previous = '-';

    foreach (var node in row)
    {
        var current = node.Value;

        if (node.IsVisited && current == '-')
        {
            continue;
        }
        else if (node.IsVisited && (current == 'F' || current == '7'))
        {
            if (previous == 'J' || previous == 'L')
            {
                crosses++;
                previous = '-';
            }
            else if (previous == 'F' || previous == '7')
            {
                previous = '-';
            }
            else
            {
                previous = current;
            }
        }
        else if (node.IsVisited && (current == 'J' || current == 'L'))
        {
            if (previous == 'F' || previous == '7')
            {
                crosses++;
                previous = '-';
            }
            else if (previous == 'J' || previous == 'L')
            {
                previous = '-';
            }
            else
            {
                previous = current;
            }
        }
        else if (node.IsVisited && current == '|')
        {
            crosses++;
            previous = '-';
        }
        else if (!node.IsVisited && crosses > 0 && crosses % 2 != 0)
        {
            enclosed++;
            pipes.MarkAsEnclosed(node);
            previous = '-';
        }
    }
}

Console.WriteLine(pipes.ToString((pipes.player.X, pipes.player.Y, 150)));
Console.WriteLine($"Enclosed: {enclosed}");

class Pipes : Grid<char>
{
    public Node<char> player;
    public Node<char> start;
    public int steps = 1;
    public List<Node<char>> enclosed;

    public Pipes(IEnumerable<IEnumerable<char>> map)
        : base(map)
    {
        player = default!;
        start = default!;
        enclosed = [];
    }

    public Node<char> MoveForward()
    {
        steps++;
        switch (player.Value)
        {
            case '|':
                if (!(this[player.X, player.Y + 1]?.IsVisited ?? true))
                {
                    player = this[player.X, player.Y + 1]
                        ?? throw new InvalidOperationException($"Attempted to set player to null position at {player.X}, {player.Y + 1}");
                }
                else
                {
                    player = this[player.X, player.Y - 1]
                        ?? throw new InvalidOperationException($"Attempted to set player to null position at {player.X}, {player.Y - 1}");
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
                        ?? throw new InvalidOperationException($"Attempted to set player to null position at {player.X}, {player.Y - 1}");
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
                    player = this[player.X, player.Y + 1]
                        ?? throw new InvalidOperationException($"Attempted to set player to null position at {player.X}, {player.Y - 1}");
                }
                break;
            default:
                throw new InvalidOperationException($"Invalid value '{player.Value}' at {player.X},{player.Y}");
        }

        player.Visit();
        return player;
    }

    public void MarkAsEnclosed(Node<char> node)
    {
        enclosed.Add(node);
    }

    public string ToString((int X, int Y, int Size) window)
    {
        StringBuilder sb = new();
        bool needsLine = false;
        Each(node =>
        {
            var chr = node.Value;
            if (node.X > window.X - window.Size && node.X < window.X + window.Size)
            {
                if (node.Y > window.Y - window.Size && node.Y < window.Y + window.Size)
                {
                    needsLine = true;
                    if (enclosed.Any(x => ReferenceEquals(x, node)))
                        sb.Append('I');
                    else if (chr == '.')
                        sb.Append(chr);
                    else if (!node.IsVisited)
                        sb.Append('0');
                    else if (ReferenceEquals(node, player))
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
            }

            if (node.X == Width - 1 && needsLine == true)
            {
                sb.AppendLine();
                needsLine = false;

            }
        });

        sb.Append(steps);
        return sb.ToString();
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
            if (lineCount != Height)
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

