using System.Text;

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
    
#if DEBUG
    public override string ToString()
    {
        var sb = new StringBuilder();

        sb.Append(Visualize(Rows[0]));
        sb.Append("|");
        sb.Append(Visualize(Rows[1]));
        sb.Append("|");
        sb.Append(Visualize(Rows[2]));
        
        return sb.ToString();

        string Visualize(Card?[] row)
        {
            return string.Join("-", row.Select(x => x?.Number.ToString() ?? "x"));
        }
    }
#endif
}