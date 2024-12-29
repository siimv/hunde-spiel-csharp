namespace ConfoundedDogGame.Players;

public static class CardMatchers
{
    public static IEnumerable<Card> FindMatchingCards(this IEnumerable<Card> cards, Side? rightSide = null, Side? bottomSide = null)
    {
        var nextCards = (rightSide, bottomSide) switch
        {
            (not null, not null) => cards.Where(x => x.HasMatchingCorner(rightSide, bottomSide)),
            (not null, _) => cards.Where(x => x.HasMatchingSide(rightSide)),
            (_, not null) => cards.Where(x => x.HasMatchingSide(bottomSide)),
            _ => []
        };
        
        foreach (var rotatedCard in nextCards.SelectMany(c => c.Rotations()))
        {
            if (IsCorrectRotation(rotatedCard))
            {
                yield return rotatedCard;
            }
        }
        
        bool IsCorrectRotation(Card nextCard)
        {
            var rightMatch = rightSide == null || rightSide.Matches(nextCard.Left);
            var bottomMatch = bottomSide == null || bottomSide.Matches(nextCard.Top);
            return rightMatch && bottomMatch;
        }
    }
}