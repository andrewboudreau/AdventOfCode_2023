# Problem Overview - Day 2
## Description
The challenge for Day 2 has not been explicitly described in your request, so I'll focus on the implementation details based on the provided solution.

## Implementation Details
- The solution involves processing a set of games, each with multiple sets of colored blocks (Red, Green, Blue).
- The primary task is to calculate two sums:
  - Check Sum: Sum of Game IDs for games where the maximum number of blocks of each color is within certain limits.
  - Power Sum: Sum of the product of the maximum numbers of Red, Green, and Blue blocks for each game.

# Solution Approach
## Key Classes and Methods
 - `Game`: A class representing a game, with methods to calculate the maximum number of blocks for each color and to parse a game from a string record.
 - `MaxHolding()`: Calculates the maximum number of Red, Green, and Blue blocks in the game.
 - `Factory(string)`: Static method to parse a game from a string record.

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

# Understanding the Game Class
## Game Factory Method
The Factory method is crucial for parsing input data into Game objects. It interprets each line of input as a different game, splitting the line into game ID and sets of colored blocks. The method then parses each set, creating a list of blocks with their number and color.

## MaxHolding Method
This method calculates the maximum number of blocks for each color present in the game. It is used to determine both the check sum and the power sum required for the challenge.