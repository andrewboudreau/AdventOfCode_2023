
var grid = new Grid<char>(Read()!);

List<Node<char>> symbols = [];
List<PartNumber> partNumbers = [];
Queue<Node<char>> builder = [];

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

// Check if any PartNumber digit is near a symbol
foreach (var partNumber in partNumbers)
{
    foreach (var digit in partNumber.PartDigits)
    {
        var neighbors = grid.Neighbors(digit);

        partNumber.IsNearSymbol =
            partNumber.PartDigits.Any(
                digit => neighbors.Any(n => symbols.Contains(n)));

        if (partNumber.IsNearSymbol)
        {
            break;
        }
    }
}

// find gears adjacent to two exactly part numbers
var gears = symbols.Where(x => x.Value == '*');
var partNumbersNearSymbols = partNumbers.Where(x => x.IsNearSymbol);
var ratioSum = 0;

foreach (var gear in gears)
{
    var partNumbersNearGear = partNumbersNearSymbols.Where(pn => pn.IsAdjacentTo(gear, grid)).ToList();
    if (partNumbersNearGear.Count == 2)
    {
        var ratio = partNumbersNearGear[0].Value * partNumbersNearGear[1].Value;
        ratioSum += ratio;

        // Console.WriteLine($"Found a gear near {gear}. Its ratio is {ratio}");
    }
}

// grid.Render(Console.WriteLine);

var missingPartNumbers = partNumbers.Where(x => !x.IsNearSymbol);
var foundPartsChecksum = partNumbers.Where(x => x.IsNearSymbol).Sum(x => x.Value);

Console.WriteLine();
Console.WriteLine($"Part 1: The missing parts checksum is {foundPartsChecksum}");
// Console.WriteLine($"for \r\n{string.Join("\r\n", missingPartNumbers)}");
Console.WriteLine($"Part 2: The checksum of all 2 ratio gears is {ratioSum}");

void AddPartNumber(Queue<Node<char>> digits)
{
    if (digits.Count == 0)
    {
        return;
    }

    var partNumber = new PartNumber();
    while (digits.TryDequeue(out var digit))
    {
        partNumber.AddDigit(digit);
    }

    partNumbers.Add(partNumber);
}

class PartNumber
{
    public int Value { get; private set; }

    public List<Node<char>> PartDigits { get; } = [];

    public bool IsNearSymbol { get; set; }

    public void AddDigit(Node<char> partsDigit)
    {
        PartDigits.Add(partsDigit);
        Value = int.Parse(Value.ToString() + partsDigit.Value);
    }

    public bool IsAdjacentTo(Node<char> symbol, Grid<char> grid)
    {
        var neighbors = grid.Neighbors(symbol);
        return PartDigits.Any(digit => neighbors.Contains(digit));
    }

    public override string ToString() => $"{PartDigits[0].X},{PartDigits[0].Y}={Value} Near={IsNearSymbol}";
}