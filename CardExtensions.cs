namespace ConfoundedDogGame;

public static class CardExtensions
{
    public static IEnumerable<Card> FindCards(this IEnumerable<Card> cards, Side? rightSide = null, Side? bottomSide = null)
    {
        var nextCards = cards
            .Where(x => rightSide == null || x.HasMatchingSide(rightSide))
            .Where(x => bottomSide == null || x.HasMatchingSide(bottomSide));

        if (rightSide != null && bottomSide != null)
        {
            nextCards = nextCards.Where(x => !x.HasMatchingCorner(rightSide, bottomSide));
        }
        
        foreach (var card in nextCards)
        {
            var nextCard = card;
            var maxRotations = 4;
            var rotations = 0;
            while (!IsCorrectRotation(nextCard))
            {
                // Card rotation cannot satisfy requirements, skip this card
                if (rotations >= maxRotations)
                {
                    nextCard = null;
                    break;
                }
                
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
            var rightMatch = rightSide == null || rightSide.Equals(nextCard.Left.Opposite());
            var bottomMatch = bottomSide == null || bottomSide.Equals(nextCard.Top.Opposite());
            return rightMatch && bottomMatch;
        }
    }
}