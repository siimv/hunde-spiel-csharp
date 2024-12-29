namespace ConfoundedDogGame;

public class Card(int number, Side a, Side b, Side c, Side d) : IEquatable<Card>
{
    public int Number { get; } = number;
    public Side Top { get; } = a;

    public Side Bottom { get; } = c;

    public Side Left { get; } = d;

    public Side Right { get; } = b;

    private Side[] AllSides => [Top, Right, Bottom, Left];

    public Card Clone()
    {
        return new Card(Number, Top, Right, Bottom, Left);
    }
    
    public Card Rotate()
    {
        return new (Number, Left, Top, Right, Bottom);
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
    
    public bool HasMatchingCorner(Side a, Side b)
    {
        for (var i = 0; i < AllSides.Length; i++)
        {
            var x = AllSides[i];
            if (!x.Equals(a.Opposite())) continue;
                
            if (NextMatch(i) || PreviousMatch(i)) return true;
        }
        return false;

        bool PreviousMatch(int index)
        {
            var previousIndex = index - 1;
            if (previousIndex < 0) previousIndex = AllSides.Length - 1;
            return AllSides[previousIndex].Equals(b.Opposite());
        }

        bool NextMatch(int index)
        {
            var nextIndex = index + 1;
            if (nextIndex >= AllSides.Length) nextIndex = 0;
            return AllSides[nextIndex].Equals(b.Opposite());
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
        return this with { Part = part };
    }

    public override string ToString()
    {
        return $"{Pattern}{Part}";
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