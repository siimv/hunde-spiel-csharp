namespace ConfoundedDogGame;

public class Board
{
    private readonly Card[][] _cards;

    public Board(Card[][] cards)
    {
        _cards = cards;
    }
    
    public IEnumerable<Card> AllCards => _cards.SelectMany(x => x);
    
    public static implicit operator Card[][](Board board)
    {
        return board._cards;
    }
}

public record Card(int Number, Side A, Side B, Side C, Side D)
{
    public Side TopSide { get; private set; } = A;

    public Side BottomSide { get; private set; } = C;

    public Side LeftSide { get; private set; } = D;

    public Side RightSide { get; private set; } = B;

    public Card Rotate()
    {
        TopSide = LeftSide;
        RightSide = TopSide;
        BottomSide = RightSide;
        LeftSide = BottomSide;
        return this;
    }
}

public record Side(Pattern Pattern, BodyPart Part);

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