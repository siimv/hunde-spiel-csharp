namespace ConfoundedDogGame.Players;

public class BruteForcePlayer
{
    /// <summary>
    /// Actual player which tries to fill all the tiles on a board correctly
    /// </summary>
    private class Player
    {
        private readonly HashSet<Card> _availableCards;
        private readonly HashSet<Card> _usedCards;
        private readonly Board _board = Board.EmptyBoard();
        
        public Player(IEnumerable<Card> cards)
        {
            _availableCards = [..cards];
            _usedCards = [];
        }

        private Player(IEnumerable<Card> availableCards, IEnumerable<Card> usedCards, Board board)
        {
            _availableCards = [..availableCards];
            _usedCards = [..usedCards];
            _board = board;
        }
        
        private Card UseCard(Card card)
        {
            _availableCards.Remove(card);
            _usedCards.Add(card);
            return card;
        }

        private static IEnumerable<Card> FindAllStartingCardCombinations(IEnumerable<Card> source)
        {
            foreach (var card in source)
            {
                var currentCard = card;
                
                // yield original card
                yield return currentCard;
                
                // yield other rotations
                foreach (var _ in Enumerable.Range(0, 3))
                {
                    currentCard = currentCard.Rotate();
                    yield return currentCard;
                }
            }
        }
        
        /// <summary>
        /// Card matchers for each specific place
        /// </summary>
        private Func<Queue<Card>>[][] NextCardAccessors =>
        [
            [
                () => new(FindAllStartingCardCombinations(_availableCards)),
                () => new(_availableCards.FindCards(_board[0, 0]!.Right)),
                () => new(_availableCards.FindCards(_board[0, 1]!.Right))
            ],
            [
                () => new(_availableCards.FindCards(bottomSide: _board[0, 0]!.Bottom)),
                () => new(_availableCards.FindCards(rightSide: _board[1, 0]!.Right, bottomSide: _board[0, 1]!.Bottom)),
                () => new(_availableCards.FindCards(rightSide: _board[1, 1]!.Right, bottomSide: _board[0, 2]!.Bottom))
            ],
            [
                () => new(_availableCards.FindCards(bottomSide: _board[1, 0]!.Bottom)),
                () => new(_availableCards.FindCards(rightSide: _board[2, 0]!.Right, bottomSide: _board[1, 1]!.Bottom)),
                () => new(_availableCards.FindCards(rightSide: _board[2, 1]!.Right, bottomSide: _board[1, 2]!.Bottom))
            ]
        ];
        
        public IEnumerable<Board> Play(Card? nextCard = null, int iStart = 0, int jStart = 0)
        {
            for (var i = iStart; i < 3; i++)
            {
                var jStartingPoint = iStart == i ? jStart : 0;
                for (var j = jStartingPoint; j < 3; j++)
                {
                    // Tile already filled
                    if (_board[i, j] != null) continue;

                    // Next card not specified, try to find possible cards
                    if (nextCard == null)
                    {
                        var nextCards = NextCardAccessors[i][j]();

                        // select a single next card and proceed with the same board
                        // go until unable to find next card
                        if (nextCards.TryDequeue(out var card))
                        {
                            nextCard = card;
                            
                            // if multiple branches (multiple next cards) then clone a board and start "sub-processes"
                            while (nextCards.TryDequeue(out var nCard))
                            {
                                var player = new Player(_availableCards, _usedCards, _board.Clone());
                                foreach (var board in player.Play(nCard, i, j))
                                {
                                    yield return board;
                                }
                            }
                        }
                    }
                    
                    // Next card found, fill the board and continue the loop
                    if (nextCard != null)
                    {
                        _board[i, j] = UseCard(nextCard);
                        nextCard = null;
                    }
                    else
                    {
                        // Cannot find suitable card, end processing
                        yield return _board;
                        yield break;
                    }
                }
            }
            
            // Return final board
            yield return _board;
        }
    }
    
    /// <summary>
    /// Returns only completed boards which are correct
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Board> Play(IEnumerable<Card> cards)
    {
        var boards = new Player(cards).Play();
        
        return boards;
    }
}