// https://adventofcode.com/2023/day/10


var map = Read(line =>
{
    return line
        .Replace("|", "│")
        .Replace("-", "─")
        .Replace("L", "└")
        .Replace("J", "┘")
        .Replace("7", "┐")
        .Replace("F", "┌");
}).ToArray();

Pipes grid = new(Read()!);
grid.Each(node =>
{
    if (node.Value == 'S')
    {
        grid.player = node;
        node.Visit();
    }
});


map.ToConsole(row => string.Join(Environment.NewLine, row));

class Pipes : Grid<char>
{
    public Node<char> player;

    public Pipes(IEnumerable<IEnumerable<char>> map)
        : base(map)
    {
    }

    //public Node<char> Next()
    //{
    //    player.
    //}
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

