// https://adventofcode.com/2023/day/7

var hands = ReadTo(lines =>
{
    var hands = new List<Hand>();
    foreach (var line in lines)
    {
        ArgumentNullException.ThrowIfNull(line);

        var data = line.Split(' ');
        hands.Add(
            new Hand(data[0], int.Parse(data[1])));
    }
    return hands;
});

int rank = 1;
long total = 0;
foreach (var hand in hands.Order())
{
    total += hand.Bid * rank;
    rank++;
    Console.WriteLine(hand);
}

Console.WriteLine(total.ToString());

//Console.WriteLine(hands.First().Type.ToString());
//Console.WriteLine(hands.Order().First().Type.ToString());

class Hand : IComparable<Hand>
{
    public Hand(string cards, int bid)
    {
        Bid = bid;
        Cards = [.. cards];
        Type = CalculateType();
    }

    private WinType CalculateType()
    {
        Dictionary<char, int> rankCounts = [];
        foreach (var card in Cards)
        {
            rankCounts.TryGetValue(card, out int value);
            rankCounts[card] = ++value;
        }

        rankCounts.TryGetValue('J', out var jokers);
        rankCounts['J'] = 0;
        var counts = new HashSet<int>(rankCounts.Values);

        if (counts.Contains(5) || jokers == 5)
        {
            return WinType.FiveOfKind;
        }
        else if (counts.Contains(4))
        {
            if (jokers == 1)
            {
                return WinType.FiveOfKind;
            }
            return WinType.FourOfKind;
        }
        else if (counts.Contains(3) && counts.Contains(2))
        {
            return WinType.FullHouse;
        }
        else if (counts.Contains(3))
        {
            if (jokers == 2)
            {
                return WinType.FiveOfKind;
            }
            else if (jokers == 1)
            {
                return WinType.FourOfKind;
            }
            return WinType.ThreeOfKind;
        }
        else if (counts.Contains(2))
        {
            if (jokers == 3)
            {
                return WinType.FiveOfKind;
            }
            else if (jokers == 2)
            {
                return WinType.FourOfKind;
            }

            if (rankCounts.Count(kvp => kvp.Value == 2) == 2)
            {
                if (jokers == 1)
                {
                    return WinType.FullHouse;
                }
                return WinType.TwoPair;
            }

            if (jokers == 1)
            {
                return WinType.ThreeOfKind;
            }

            return WinType.OnePair;
        }
        else if (jokers == 4)
        {
            return WinType.FiveOfKind;
        }
        else if (jokers == 3)
        {
            return WinType.FourOfKind;
        }
        else if (jokers == 2)
        {
            return WinType.ThreeOfKind;
        }
        else if (jokers == 1)
        {
            return WinType.OnePair;
        }

        return WinType.HighCard;
    }

    public int CompareTo(Hand? other)
    {
        ArgumentNullException.ThrowIfNull(other);

        if (Type == other.Type)
        {
            using var otherCards = other.Cards.GetEnumerator();
            foreach (var card in Cards)
            {
                otherCards.MoveNext();
                if (card == otherCards.Current)
                {
                    continue;
                }
                if (card == 'J')
                {
                    return -1;
                }
                if (otherCards.Current == 'J')
                {
                    return 1;
                }
                if (char.IsLetter(card) && char.IsDigit(otherCards.Current))
                {
                    return 1;
                }
                if (char.IsDigit(card) && char.IsLetter(otherCards.Current))
                {
                    return -1;
                }
                if (char.IsDigit(card) && char.IsDigit(otherCards.Current))
                {
                    return int.Parse(card.ToString()).CompareTo(int.Parse(otherCards.Current.ToString()));
                }
                if (card == 'A')
                {
                    return 1;
                }
                if (otherCards.Current == 'A')
                {
                    return -1;
                }
                if (card == 'K')
                {
                    return 1;
                }
                if (otherCards.Current == 'K')
                {
                    return -1;
                }
                if (card == 'Q')
                {
                    return 1;
                }
                if (otherCards.Current == 'Q')
                {
                    return -1;
                }
            }

            return 0;
        }

        return Type.CompareTo(other.Type);
    }

    public int Bid { get; }
    public List<char> Cards { get; }
    public WinType Type { get; }

    public override string ToString()
    {
        return $"{Type} {string.Join("", Cards)} {Bid}";
    }
}

enum WinType
{
    HighCard = 1,
    OnePair = 2,
    TwoPair = 3,
    ThreeOfKind = 4,
    FullHouse = 5,
    FourOfKind = 6,
    FiveOfKind = 7
}