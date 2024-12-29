namespace ConfoundedDogGame;

public static class CorrectnessHelpers
{
    /// <summary>
    /// Verifies that the all tiles on a board are filled
    /// </summary>
    /// <param name="board"></param>
    /// <returns></returns>
    public static bool IsComplete(this Board board) => board.Rows.All(r => r.All(x => x != null));
    
    /// <summary>
    /// This is just to validate the correctness of the board
    /// </summary>
    /// <returns></returns>
    public static bool IsCorrect(this Board board)
    {
        if (!board.IsComplete()) return false;

        var cards = board.Rows;
        
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
}