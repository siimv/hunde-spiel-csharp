using System.Text;

namespace ConfoundedDogGame.Visualizers;

public class DefaultVisualizer : IVisualizer
{
    public string Visualize(Card? card)
    {
        return card?.ToString() ?? "??";
    }

    public string VisualizeBoard(Board board)
    {
        var sb = new StringBuilder();

        sb.Append($"[{board.IsComplete()}]");
        sb.Append(Visualize(board.Rows[0]));
        sb.Append("|");
        sb.Append(Visualize(board.Rows[1]));
        sb.Append("|");
        sb.Append(Visualize(board.Rows[2]));
        
        return sb.ToString();
    }

    private string Visualize(Card?[] row)
    {
        return string.Join("-", row.Select(x => x?.Number.ToString() ?? "x"));
    }
}