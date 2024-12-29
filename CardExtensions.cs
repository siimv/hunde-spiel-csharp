namespace ConfoundedDogGame;

public static class CardExtensions
{
    public static IEnumerable<Card> FindCards(this IEnumerable<Card> cards, Side? rightSide = null, Side? bottomSide = null, Side? topSide = null)
    {
        var nextCards = cards
            .Where(x => rightSide == null || x.HasMatchingSide(rightSide))
            .Where(x => bottomSide == null || x.HasMatchingSide(bottomSide))
            .Where(x => topSide == null || x.HasMatchingSide(topSide));

        if (rightSide != null && bottomSide != null)
        {
            nextCards = nextCards.Where(x => !x.HasMatchingCorner(rightSide, bottomSide));
        }

        if (rightSide != null && topSide != null)
        {
            nextCards = nextCards.Where(x => !x.HasMatchingCorner(rightSide, topSide));
        }

        if (topSide != null && bottomSide != null)
        {
            nextCards = nextCards.Where(x => !x.HasMatchingSides(topSide, bottomSide));
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
            var rightMatch = rightSide == null || rightSide.Equals(nextCard.LeftSide.Opposite());
            var bottomMatch = bottomSide == null || bottomSide.Equals(nextCard.TopSide.Opposite());
            var topMatch = topSide == null || topSide.Equals(nextCard.BottomSide.Opposite());
            return rightMatch && bottomMatch && topMatch;
        }
    }
}