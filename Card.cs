namespace ConfoundedDogGame;

public class Board
{
    private readonly Card?[][] _cards;

    public Board(Card?[][] cards)
    {
        _cards = cards;
    }
    
    public Card?[][] Rows => _cards;
    
    // public bool IsComplete => Rows.Sum(r => r.Count(x => x != null)) == 6;
    public bool IsComplete => Rows.All(r => r.All(x => x != null));

    /// <summary>
    /// This is just to validate the correctness of the board
    /// </summary>
    /// <returns></returns>
    public bool IsCorrect()
    {
        if (!IsComplete) return false;

        // First row
        if (!_cards[0][0]!.RightSide.Equals(_cards[0][1]!.LeftSide.Opposite())) return false;
        if (!_cards[0][1]!.RightSide.Equals(_cards[0][2]!.LeftSide.Opposite())) return false;
        
        // Second row
        if (!_cards[1][0]!.RightSide.Equals(_cards[1][1]!.LeftSide.Opposite())) return false;
        if (!_cards[1][0]!.TopSide.Equals(_cards[0][0]!.BottomSide.Opposite())) return false;
        
        if (!_cards[1][1]!.RightSide.Equals(_cards[1][2]!.LeftSide.Opposite())) return false;
        if (!_cards[1][1]!.TopSide.Equals(_cards[0][1]!.BottomSide.Opposite())) return false;
        
        if (!_cards[1][2]!.TopSide.Equals(_cards[0][2]!.BottomSide.Opposite())) return false;
        
        // Third row
        if (!_cards[2][0]!.RightSide.Equals(_cards[2][1]!.LeftSide.Opposite())) return false;
        if (!_cards[2][0]!.TopSide.Equals(_cards[1][0]!.BottomSide.Opposite())) return false;
        
        if (!_cards[2][1]!.RightSide.Equals(_cards[2][2]!.LeftSide.Opposite())) return false;
        if (!_cards[2][1]!.TopSide.Equals(_cards[1][1]!.BottomSide.Opposite())) return false;
        
        if (!_cards[2][2]!.TopSide.Equals(_cards[1][2]!.BottomSide.Opposite())) return false;
        
        return true;
    }
    
    public Board Clone()
    {
        return new(_cards.Select(x => x.Select(c => c?.Clone()).ToArray()).ToArray());
    }
    
    public static Board EmptyBoard()
    {
        return new([
            new Card[3],
            new Card[3],
            new Card[3],
        ]);
    }

    public Card? this[int i, int j]
    {
        get => _cards[i][j];
        set => _cards[i][j] = value;
    }
    
    public static implicit operator Card[][](Board board)
    {
        return board._cards;
    }
}

public class Card(int number, Side a, Side b, Side c, Side d) : IEquatable<Card>
{
    public int Number { get; } = number;
    public Side TopSide { get; private set; } = a;

    public Side BottomSide { get; private set; } = c;

    public Side LeftSide { get; private set; } = d;

    public Side RightSide { get; private set; } = b;

    private Side[] AllSides => [a, b, c, d];

    public Card Clone()
    {
        return new Card(Number, a, b, c, d);
    }
    
    public Card Rotate()
    {
        return new (Number, LeftSide, TopSide, RightSide, BottomSide);
    }

    public bool Equals(Card? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return Number == other.Number;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((Card)obj);
    }

    public override int GetHashCode()
    {
        return Number;
    }

    public bool HasMatchingSide(Side side)
    {
        return AllSides.Contains(side.Opposite());
    }

    public bool HasMatchingSides(Side a, Side b)
    {
        for (int i = 0; i < AllSides.Length; i++)
        {
            var x = AllSides[i];
            if (!x.Equals(a.Opposite())) continue;

            var otherSide = i switch
            {
                0 => 2,
                1 => 3,
                2 => 0,
                3 => 1
            };

            return AllSides[otherSide].Equals(b.Opposite());
        }
        return false;
    }
    
    public bool HasMatchingCorner(Side a, Side b)
    {
        for (int i = 0; i < AllSides.Length; i++)
        {
            var x = AllSides[i];
            if (!x.Equals(a.Opposite())) continue;
            
            if (i == 0)
            {
                return NextMatch(i);
            }
            if (i == AllSides.Length - 1)
            {
                return PreviousMatch(i);
            }
                
            if (NextMatch(i) || PreviousMatch(i)) return true;
        }
        return false;

        bool PreviousMatch(int index)
        {
            return AllSides[index - 1].Equals(b.Opposite());
        }

        bool NextMatch(int index)
        {
            return AllSides[index + 1].Equals(b.Opposite());
        }
    }
}

public record Side(Pattern Pattern, BodyPart Part)
{
    public Side Opposite()
    {
        var part = Part switch
        {
            BodyPart.Head => BodyPart.Tail,
            BodyPart.Tail => BodyPart.Head,
        };
        return new (Pattern, part);
    }
}

public enum BodyPart
{
    Head,
    Tail
}

public enum Pattern
{
    Brown,
    Grey,
    Spotted,
    Umber
}