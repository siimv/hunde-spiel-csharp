namespace ConfoundedDogGame.Visualizers;

public class VisualEqualityComparer : IEqualityComparer<Board>
{
    public bool Equals(Board? x, Board? y)
    {
        if (x is null || y is null) return false;
        
        return GetRotatedBoards(y).Any(other => IsEquivalent(x, other));
    }

    private bool IsEquivalent(Board current, Board other)
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                var a = current[i, j];
                var b = other[i, j];

                if (a == null || b == null) return false;
                if (!IsVisuallyEquivalent(a, b)) return false;
            }
        }

        return true;
    }
    
    public int GetHashCode(Board obj)
    {
        var code = new HashCode();

        //TODO: This is kind of accidental, when changing the order by, it produces different result
        foreach (var rotation in GetRotatedBoards(obj).OrderBy(x => x.Rows[0][1]!.Number))
        {
            foreach (var card in rotation.Rows.SelectMany(x => x))
            {
                code.Add(CardHashCode(card));
            }
        }
        
        var hashCode = code.ToHashCode();
        return hashCode;
    }

    private static IEnumerable<Board> GetRotatedBoards(Board board)
    {
        const int maxRotations = 4;
        var lastRotation = board;
        
        var rotations = 0;
        do
        {
            yield return lastRotation;
            lastRotation = RotateBoard(lastRotation);
            rotations++;
        }
        while(rotations < maxRotations);
    }

    private static Board RotateBoard(Board board)
    {
        var newBoard = Board.EmptyBoard();

        newBoard[0, 0] = board[2, 0]!.Rotate();
        newBoard[0, 1] = board[1, 0]!.Rotate();
        newBoard[0, 2] = board[0, 0]!.Rotate();
        
        newBoard[1, 0] = board[2, 1]!.Rotate();
        newBoard[1, 1] = board[1, 1]!.Rotate();
        newBoard[1, 2] = board[0, 1]!.Rotate();
        
        newBoard[2, 0] = board[2, 2]!.Rotate();
        newBoard[2, 1] = board[1, 2]!.Rotate();
        newBoard[2, 2] = board[0, 2]!.Rotate();
        
        return newBoard;
    }
    
    private static bool IsVisuallyEquivalent(Card a, Card b)
    {
        // If physically same card
        if (a.Equals(b)) return true;

        return HasSameSides(a, b);
    }

    private static bool HasSameSides(Card a, Card b)
    {
        if (!a.Top.Equals(b.Top)) return false;
        if (!a.Bottom.Equals(b.Bottom)) return false;
        if (!a.Right.Equals(b.Right)) return false;
        if (!a.Left.Equals(b.Left)) return false;

        return true;
    }

    private static int CardHashCode(Card? c)
    {
        return c == null ? 0 : HashCode.Combine(c.Top, c.Bottom, c.Right, c.Left);
    }
}