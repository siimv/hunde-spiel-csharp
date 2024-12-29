namespace ConfoundedDogGame;

public class Board(Card?[][] cards)
{
    public Card?[][] Rows => cards;
    
    public Board Clone()
    {
        return new(cards.Select(x => x.Select(c => c?.Clone()).ToArray()).ToArray());
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
        get => cards[i][j];
        set => cards[i][j] = value;
    }
}