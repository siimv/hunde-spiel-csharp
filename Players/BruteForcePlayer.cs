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
        private readonly Card _startingCard;
        private readonly HashSet<Card> _availableCards;
        private readonly HashSet<Card> _usedCards;
        private readonly Board _board = Board.EmptyBoard();
        
        public Player(IEnumerable<Card> cards, Card startingCard)
        {
            _startingCard = startingCard;
            _availableCards = [..cards];
            _usedCards = [];
        }

        private Player(IEnumerable<Card> availableCards, IEnumerable<Card> usedCards, Board board, Card startingCard)
        {
            _availableCards = new(availableCards);
            _usedCards = new(usedCards);
            _board = board;
            _startingCard = startingCard;
        }
        
        private Card UseCard(Card card)
        {
            _availableCards.Remove(card);
            _usedCards.Add(card);
            return card;
        }
        
        private IEnumerable<Board> Process(int i, int j, CardWrapper nextCard, Func<Board, IEnumerable<Card>>[][] nextCardAccessor)
        {
            if (_board[i, j] != null) yield break;
            
            if (nextCard.Card != null)
            {
                _board[i, j] = UseCard(nextCard.Card);
                nextCard.Card = null;

                yield return _board;
                yield break;
            }
            else
            {
                var accessor = nextCardAccessor[i][j](_board);
                foreach (var board in FindOtherBoards(accessor))
                {
                    yield return board;
                }
            }
            
            IEnumerable<Board> FindOtherBoards(IEnumerable<Card> nextCards)
            {
                foreach (var card in nextCards)
                {
                    var player = new Player(_availableCards, _usedCards, _board.Clone(), _startingCard);
                    foreach (var board in player.Play(new(card)))
                    {
                        yield return board;
                    }
                }
            }
        }

        private class CardWrapper(Card? card = null)
        {
            public Card? Card { get; set; } = card;
        }

        public IEnumerable<Board> Play()
        {
            return Play(new());
        }
        
        private Func<Board, Queue<Card>>[][] _nextCardAccessors =>
        [
            [
                b => new([_startingCard]),
                b => new(_availableCards.FindCards(b[0, 0]!.RightSide)),
                b => new(_availableCards.FindCards(b[0, 1]!.RightSide))
            ],
            [
                b => new(_availableCards.FindCards(bottomSide: b[0, 0]!.BottomSide)),
                b => new(_availableCards.FindCards(rightSide: b[1, 0]!.RightSide, bottomSide: b[0, 1]!.BottomSide)),
                b => []
            ],
            [
                b => [],
                b => [],
                b => []
            ]
        ];
        
        private IEnumerable<Board> Process2(int i, int j, CardWrapper nextCard, Func<Board, IEnumerable<Card>>[][] nextCardAccessor)
        {
            if (_board[i, j] != null) yield break;
            
            if (nextCard.Card != null)
            {
                _board[i, j] = UseCard(nextCard.Card);
                nextCard.Card = null;

                yield return _board;
                yield break;
            }
            else
            {
                var accessor = nextCardAccessor[i][j](_board);
                foreach (var board in FindOtherBoards(accessor))
                {
                    yield return board;
                }
            }
            
            IEnumerable<Board> FindOtherBoards(IEnumerable<Card> nextCards)
            {
                foreach (var card in nextCards)
                {
                    var player = new Player(_availableCards, _usedCards, _board.Clone(), _startingCard);
                    foreach (var board in player.Play(new(card)))
                    {
                        yield return board;
                    }
                }
            }
        }
        
        private IEnumerable<Board> Play(CardWrapper nextCard, int iStart = 0, int jStart = 0)
        {
            for (int i = iStart; i < 3; i++)
            {
                for (int j = jStart; j < 3; j++)
                {
                    // Tile already filled
                    if (_board[i, j] != null) continue;

                    if (nextCard.Card == null)
                    {
                        var nextCards = _nextCardAccessors[i][j](_board);

                        // select a single next card and proceed with the same board
                        // go until unable to find next card
                        if (nextCards.TryDequeue(out var card))
                        {
                            nextCard.Card = card;
                            
                            // if multiple branches (multiple next cards) then clone a board and start "sub-processes"
                            while (nextCards.TryDequeue(out var nCard))
                            {
                                var player = new Player(_availableCards, _usedCards, _board.Clone(), _startingCard);
                                foreach (var board in player.Play(new(nCard), i, j))
                                {
                                    yield return board;
                                }
                            }
                        }
                        
                        
                        //TODO: Process other boards
                        // foreach (var card in nextCards)
                        // {
                        //     var player = new Player(_availableCards, _usedCards, _board.Clone(), _startingCard);
                        //     foreach (var board in player.Play(new(card)))
                        //     {
                        //         yield return board;
                        //     }
                        // }
                    }
                    
                    if (nextCard.Card != null)
                    {
                        _board[i, j] = UseCard(nextCard.Card);
                        nextCard.Card = null;

                        // yield return _board;
                        // yield break;
                    }
                    else
                    {
                        // Cannot proceed, end processing
                        yield return _board;
                        yield break;
                    }
                    
                    // if multiple branches (multiple next cards) then clone a board and start "sub-processes"
                    // foreach (var board in Process(i, j, nextCard, nextCardAccessors))
                    // {
                    //     yield return board;
                    // }
                }

                yield return _board;
                yield break;
            }
            
yield return _board;
        }
    }
    
    public IReadOnlyCollection<Board> Play()
    {
        List<Board> boards = [];

        for (int i = 0; i < _cards.Length; i++)
        {
            boards.AddRange(new Player(_cards, _cards[i]).Play());
            if (i==0) break;
        }

        Console.WriteLine($"Number of boards: {boards.Count}");
        return boards;
        // var board = boards
        //     .OrderByDescending(x => x.Rows.Sum(r => r.Count(c => c != null)))
        //     .FirstOrDefault();
        // return board;
    }
}