namespace ConfoundedDogGame;

public class Board(Card?[][] cards)
{
    public Card?[][] Rows => cards;
    
    public bool IsComplete => Rows.All(r => r.All(x => x != null));

    /// <summary>
    /// This is just to validate the correctness of the board
    /// </summary>
    /// <returns></returns>
    public bool IsCorrect()
    {
        if (!IsComplete) return false;

        // First row
        if (!cards[0][0]!.Right.Equals(cards[0][1]!.Left.Opposite())) return false;
        if (!cards[0][1]!.Right.Equals(cards[0][2]!.Left.Opposite())) return false;
        
        // Second row
        if (!cards[1][0]!.Right.Equals(cards[1][1]!.Left.Opposite())) return false;
        if (!cards[1][0]!.Top.Equals(cards[0][0]!.Bottom.Opposite())) return false;
        
        if (!cards[1][1]!.Right.Equals(cards[1][2]!.Left.Opposite())) return false;
        if (!cards[1][1]!.Top.Equals(cards[0][1]!.Bottom.Opposite())) return false;
        
        if (!cards[1][2]!.Top.Equals(cards[0][2]!.Bottom.Opposite())) return false;
        
        // Third row
        if (!cards[2][0]!.Right.Equals(cards[2][1]!.Left.Opposite())) return false;
        if (!cards[2][0]!.Top.Equals(cards[1][0]!.Bottom.Opposite())) return false;
        
        if (!cards[2][1]!.Right.Equals(cards[2][2]!.Left.Opposite())) return false;
        if (!cards[2][1]!.Top.Equals(cards[1][1]!.Bottom.Opposite())) return false;
        
        if (!cards[2][2]!.Top.Equals(cards[1][2]!.Bottom.Opposite())) return false;
        
        return true;
    }
    
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