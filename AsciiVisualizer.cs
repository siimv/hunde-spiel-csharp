using System.Text;

namespace ConfoundedDogGame;

public class AsciiVisualizer : IVisualizer
{
    public string VisualizeBoard(Card[][] board)
    {
        var sb = new StringBuilder();

        sb.Append(Visualize(board[0]));
        sb.AppendLine();
        sb.Append(Visualize(board[1]));
        sb.AppendLine();
        sb.Append(Visualize(board[2]));
        
        return sb.ToString();
    }

    private string Visualize(Card[] cards)
    {
        var card1 = cards[0];
        var card2 = cards[1];
        var card3 = cards[2];
        
        var emptyRow = $"{Empty()}  {Empty()}  {Empty()}\n";
        var sb = new StringBuilder();
        
        sb.AppendFormat("{0}  {1}  {2}\n", Top(card1), Top(card2), Top(card3));
        sb.AppendFormat(emptyRow);
        sb.AppendFormat("{0} {1} {2}\n", Middle(card1), Middle(card2), Middle(card3));
        sb.AppendFormat(emptyRow);
        sb.AppendFormat("{0}  {1}  {2}", Bottom(card1), Bottom(card2), Bottom(card3));
        
        return sb.ToString();

        string Top(Card card)
        {
            return $"--{Map(card.TopSide)}--";
        }

        string Middle(Card card)
        {
            return $"{Map(card.LeftSide)} {card.Number} {Map(card.RightSide)}";
        }
        
        string Empty()
        {
            return "|    |";
        }
        
        string Bottom(Card card)
        {
            return $"--{Map(card.BottomSide)}--";
        }
    }
    
    public string Visualize(Card card)
    {
        var sb = new StringBuilder();

        sb.AppendFormat("--{0}--\n", Map(card.TopSide));
        sb.AppendFormat("|    |\n");
        sb.AppendFormat("{0}   {1}\n", Map(card.LeftSide), Map(card.RightSide));
        sb.AppendFormat("|    |\n");
        sb.AppendFormat("--{0}--", Map(card.BottomSide));
        
        return sb.ToString();
    }

    private string Map(Side side)
    {
        return $"{Map(side.Pattern)}{Map(side.Part)}";
    }
    
    private string Map(BodyPart part)
    {
        return part switch
        {
            BodyPart.Head => "H",
            BodyPart.Tail => "T",
        };
    }
    
    private string Map(Pattern pattern)
    {
        return pattern switch
        {
            Pattern.Brown => "B",
            Pattern.Grey => "G",
            Pattern.Spotted => "S",
            Pattern.Umber => "M",
        };
    }
}