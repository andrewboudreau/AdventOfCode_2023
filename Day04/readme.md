# Problem Overview - Day 4
## Description
The challenge for Day 4 involves processing a collection of scratch cards, each with its own set of numbers and winning numbers.

## Key Tasks
 - Calculate a total score based on the scratch cards' numbers.
 - Simulate a series of games using the scratch cards, keeping track of played and unplayed games.

# Solution Approach
## Key Classes and Concepts
 - ScratchCard: Represents a scratch card with a set of numbers and winning numbers.
 - Game: Represents a game session using a scratch card.

## Score Calculation and Game Simulation
The solution involves creating scratch cards from input data, calculating a total score, and simulating game sessions with these cards. The games are played in a specific order, influenced by the results of each game.

## Example Usages

### Reading Scratch Cards and Calculating Score
```csharp
var cards = Read(factory: line => new ScratchCard(line));
Console.WriteLine($"Part 1: Total score is {cards.Sum(x => x.Score)}.");
```

### Game Simulation Logic
```csharp
var games = cards.Select(card => new Game(card)).ToList();
...
do
{
    ...
} while (games[index].Unplayed != 0);

Console.WriteLine($"There are {games.Sum(g => g.Played)} total cards.");
```

### ScratchCard and Game Classes
```csharp
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
    ...

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
```

# Understanding the Solution
## ScratchCard Class
The ScratchCard class is central to the solution. It parses each line of the input into a scratch card, splitting the line into the card's ID, your numbers, and winning numbers. It also calculates the number of winning numbers and the score for the card.

## Game Class and Simulation
The Game class wraps a ScratchCard and simulates playing the card. It keeps track of the number of times the card has been played and unplayed. The game logic involves playing each card and affecting other cards based on the results.