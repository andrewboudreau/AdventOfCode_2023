# Problem Overview - Day 3

## Description
The challenge for Day 3 appears to involve processing a grid of characters, identifying part numbers, symbols, 
and their relationships.

# Key Tasks
 - Parse part numbers and symbols from a grid.
 - Determine relationships between part numbers and symbols.
 - Calculate checksums based on these relationships.

# Solution Approach
## Key Classes and Concepts
 - Grid<char>: Represents the grid of characters.
 - PartNumber: A class to represent a part number, which consists of digits found in the grid.
 - Node<char>: Represents a cell in the grid.

## Parsing Part Numbers and Symbols
The solution involves iterating over the grid to identify part numbers (sequences of digits) and symbols (special characters). Part numbers are built by enqueueing digit cells, and symbols are collected separately.

## Relationship Analysis
For each part number, the solution checks whether any of its digits is near a symbol. Additionally, it finds gears (* symbols) adjacent to exactly two part numbers and calculates a ratio sum based on these relationships.

## Example Usages
Parsing Grid and Building Part Numbers

```csharp
var grid = new Grid<char>(Read()!);
...
// Parse parts numbers and symbols
grid.Each(cell =>
{
    ...
});
```

Relationship Analysis and Calculations
```csharp
// Check if any PartNumber digit is near a symbol
...
// find gears adjacent to two exactly part numbers
...

Console.WriteLine($"Part 1: The missing parts checksum is {foundPartsChecksum}");
Console.WriteLine($"Part 2: The checksum of all 2 ratio gears is {ratioSum}");

```

PartNumber Class and Adjacency Logic
```csharp
class PartNumber
{
    ...
    public bool IsAdjacentTo(Node<char> symbol, Grid<char> grid)
    {
        ...
    }
    ...
}
```

# Understanding the Solution
## Parsing and Building Logic
The parsing logic involves examining each cell in the grid to determine if it's part of a part number or a symbol. Part numbers are built by aggregating consecutive digit cells.

## Relationship and Adjacency Logic
The relationship analysis is crucial to solving the problem. It involves determining if part numbers are adjacent to symbols and calculating sums based on specific adjacency criteria (e.g., gears adjacent to exactly two part numbers).