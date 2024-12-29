namespace ConfoundedDogGame.Players;

public static class CardExtensions
{
    public static IEnumerable<Card> FindCards(this IEnumerable<Card> cards, Side? rightSide = null, Side? bottomSide = null)
    {
        var nextCards = (rightSide, bottomSide) switch
        {
            (not null, not null) => cards.Where(x => x.HasMatchingCorner(rightSide, bottomSide)),
            (not null, _) => cards.Where(x => x.HasMatchingSide(rightSide)),
            (_, not null) => cards.Where(x => x.HasMatchingSide(bottomSide)),
            _ => []
        };
        
        const int maxRotations = 4;
        foreach (var card in nextCards)
        {
            var nextCard = card;
            var rotations = 0;
            while (!IsCorrectRotation(nextCard))
            {
                // Card rotation cannot satisfy requirements, skip this card
                if (rotations >= maxRotations)
                {
                    nextCard = null;
                    break;
                }
                
                // Rotate card and check again
                nextCard = nextCard.Rotate();
                rotations++;
            }

            if (nextCard != null)
            {
                yield return nextCard;
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