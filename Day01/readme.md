# Advent of Code 2023 - Day 1 Solution
## Problem Overview - Day 1: Trebuchet?!
## Description
In this challenge, the Elves' calibration document for snow production has been altered by artistic embellishments, rendering the calibration values unreadable. Each line of the document originally contained a specific calibration value, discernible by combining the first and last digits (in that order) to form a two-digit number.

## Part One
For Part One, the task is to recover the calibration values from each line and sum them up. The calibration value is formed by the first and last numeric digits on each line.

## Part Two
In Part Two, the complexity increases as some of the digits are spelled out with letters (like 'one', 'two', etc.). The task remains the same: to find the real first and last digit on each line and sum up the calibration values.

## Solution Approach
The solution involves processing each line of the input to extract the first and last digits, whether they are numeric or spelled out in text, and then combining these digits to calculate a checksum.

# Key Methods
 - `Read(Func<string, string>)` Reads each line from the input.
 - `Aggregate(int, Func<int, string, int>)` Aggregates the calculated values from each line.
 - `FindFirstDigit(string)` Finds the first digit in the line.
 - `FindLastDigit(string)` Finds the last digit in the line.
 - `TryGetDigit(string, int, out int)` Tries to get a digit from a given position in the string.

## Example Usages
### Part One - Numeric Digits Only
```csharp
Read(input => input!)
    .Aggregate(0,
        (checksum, lineOfText) =>
        {
            var first = lineOfText.FirstOrDefault(x => char.IsDigit(x)) - '0';
            var last = lineOfText.LastOrDefault(x => char.IsDigit(x)) - '0';
            var value = first * 10 + last;
            checksum += value;
            return checksum;
        })
    .ToConsole(sum => $"Part1: {sum}\r\n");
```

### Part Two - Numeric and Spelled-Out Digits
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

### Digit Recognition Logic
```csharp
bool TryGetDigit(string line, int offset, out int digit)
{
    Dictionary<string, int> stringDigitMapping = new() { ["one"] = 1, ["two"] = 2, ... };

    digit = 0;
    if (offset >= line.Length)
    {
        throw new ArgumentOutOfRangeException(...);
    }

    if (char.IsDigit(line[offset]))
    {
        digit = line[offset] - '0';
        return true;
    }

    foreach (var pair in stringDigitMapping)
    {
        if (offset + pair.Key.Length <= line.Length && line.Substring(offset, pair.Key.Length) == pair.Key)
        {
            digit = pair.Value;
            return true;
        }
    }

    return false;
}
```

# Final Notes
This solution demonstrates a robust approach to handling string manipulation and conditional logic in C#. It showcases the ability to adapt the solution to increased complexity, as seen in Part Two of the challenge.