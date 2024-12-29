namespace ConfoundedDogGame.Players;

public class BruteForcePlayer
{
    private readonly Card[] _cards;

    public BruteForcePlayer(IEnumerable<Card> cards)
    {
        _cards = [..cards];
    }

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
            _availableCards = new(availableCards);
            _usedCards = new(usedCards);
            _board = board;
        }
        
        private Card UseCard(Card card)
        {
            _availableCards.Remove(card);
            _usedCards.Add(card);
            return card;
        }

        public IEnumerable<Board> Play()
        {
            return Play(null);
        }
        
        private Func<Board, Queue<Card>>[][] NextCardAccessors =>
        [
            [
                b => new(_availableCards),
                b => new(_availableCards.FindCards(b[0, 0]!.RightSide)),
                b => new(_availableCards.FindCards(b[0, 1]!.RightSide))
            ],
            [
                b => new(_availableCards.FindCards(bottomSide: b[0, 0]!.BottomSide)),
                b => new(_availableCards.FindCards(rightSide: b[1, 0]!.RightSide, bottomSide: b[0, 1]!.BottomSide)),
                // b => []
                b => new(_availableCards.FindCards(rightSide: b[1, 1]!.RightSide, bottomSide: b[0, 2]!.BottomSide))
            ],
            [
                // b => [],
                // b => [],
                // b => []
                b => new(_availableCards.FindCards(bottomSide: b[1, 0]!.BottomSide)),
                b => new(_availableCards.FindCards(rightSide: b[2, 0]!.RightSide, bottomSide: b[1, 1]!.BottomSide)),
                b => new(_availableCards.FindCards(rightSide: b[2, 1]!.RightSide, bottomSide: b[1, 2]!.BottomSide))
            ]
        ];
        
        private IEnumerable<Board> Play(Card? nextCard, int iStart = 0, int jStart = 0)
        {
            for (int i = iStart; i < 3; i++)
            {
                var jStartingPoint = iStart == i ? jStart : 0;
                for (int j = jStartingPoint; j < 3; j++)
                {
                    // Tile already filled
                    if (_board[i, j] != null) continue;

                    // Next card not specified, try to find possible cards
                    if (nextCard == null)
                    {
                        var nextCards = NextCardAccessors[i][j](_board);

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
    
    public IReadOnlyCollection<Board> Play()
    {
        var boards = new Player(_cards).Play()
            // .Where(x => x.IsComplete)
            // .Where(x => x.IsCorrect())
            .ToArray();

        Console.WriteLine($"Total nr of boards: {boards.Length}");
        Console.WriteLine($"Completed nr of boards: {boards.Count(x => x.IsComplete)}");
        Console.WriteLine($"Correct nr of boards: {boards.Count(x => x.IsCorrect())}");
        return boards.Where(x => x.IsComplete).ToArray();
    }
}