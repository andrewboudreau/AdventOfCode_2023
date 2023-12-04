# AdventOfCode 2023

[2023 Advent of Code](https://adventofcode.com) Solutions in C#

This repository contains my personal solutions for the Advent of Code challenges, implemented in C#. As an advanced C# software engineer, I've approached these puzzles with a focus on efficient and effective programming techniques. Each folder in the repository corresponds to a specific day of the challenge, containing the C# code that I've written to solve the daily puzzles. This is not just a collection of solutions, but a reflection of my problem-solving journey and coding skills in C#. Feel free to explore my approaches and share your thoughts or alternative solutions!

# Day 01
 [Read the full details](Day01/readme.md) about the solution.
## Part Two - Numeric and Spelled-Out Digits
```csharp
Read(input => input!)
    .Aggregate(0,
        (checksum, lineOfText) =>
        {
            var first = FindFirstDigit(lineOfText);
            var last = FindLastDigit(lineOfText);
            var value = first * 10 + last;

            Console.WriteLine($"{lineOfText}: {value}");
            checksum += value;
            return checksum;
        })
    .ToConsole(sum => $"Part2: {sum}");
```


# Day 02
 [Read the full details](Day02/readme.md) about the solution.
## Reading and Processing Games
```csharp
var games = Read(factory: Game.Factory).ToList();

var checkSum = 0;
var powerSum = 0;
var min = (Red: 12, Green: 13, Blue: 14);

foreach (var game in games)
{
    var (Red, Green, Blue) = game.MaxHolding();
    // Calculating sums based on the game's characteristics
    ...
}
```

## Game Class and Parsing Logic
```csharp
class Game(int gameId)
{
    ...
    public static Game Factory(string record)
    {
        ...
        // Parsing logic for each game
    }
    ...
}
```

# Day 03
 [Read the full details](Day03/readme.md) about the solution.
 First usage of the Grid class as seen
 ```csharp
var grid = new Grid<char>(Read());

...
// Parse parts numbers and symbols
grid.Each(cell =>
{
    if (char.IsDigit(cell))
    {
        builder.Enqueue(cell);
    }
    else
    {
        AddPartNumber(builder);
        if (cell != '.')
        {
            symbols.Add(cell);
        }
    }
});
```

Relationship Analysis and Calculations
```csharp

// find gears adjacent to two exactly part numbers
class PartNumber
{
    ...
    public bool IsAdjacentTo(Node<char> symbol, Grid<char> grid)
    {
        var neighbors = grid.Neighbors(symbol);
        return PartDigits.Any(digit => neighbors.Contains(digit));
    }
    ...
}
```