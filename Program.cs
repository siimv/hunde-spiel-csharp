// See https://aka.ms/new-console-template for more information

using ConfoundedDogGame;
using ConfoundedDogGame.Players;
using ConfoundedDogGame.Visualizers;

// var visualizer = new DefaultVisualizer();
var visualizer = new AsciiVisualizer();
var game = new Game();

var player = new BruteForcePlayer(game.Tiles);
var boards = player.Play();

// Console.WriteLine(visualizer.Visualize(board.Tiles.First()));
foreach (var board in boards)
{
    Console.WriteLine("----------");
    Console.WriteLine(visualizer.VisualizeBoard(board));
}

public class Game
{
    public Card[] Tiles { get; }
    
    public Board Board { get; }

    public Game()
    {
        Tiles =
        [
            new(1, 
                new(Pattern.Brown, BodyPart.Head),
                new(Pattern.Grey, BodyPart.Head),
                new(Pattern.Umber, BodyPart.Tail),
                new(Pattern.Spotted, BodyPart.Tail)
            ),
            new(2, 
                new(Pattern.Brown, BodyPart.Head),
                new(Pattern.Spotted, BodyPart.Head),
                new(Pattern.Brown, BodyPart.Tail),
                new(Pattern.Umber, BodyPart.Tail)
            ),
            new(3, 
                new(Pattern.Brown, BodyPart.Head),
                new(Pattern.Spotted, BodyPart.Head),
                new(Pattern.Grey, BodyPart.Tail),
                new(Pattern.Umber, BodyPart.Tail)
            ),
            new(4, 
                new(Pattern.Brown, BodyPart.Head),
                new(Pattern.Spotted, BodyPart.Head),
                new(Pattern.Grey, BodyPart.Tail),
                new(Pattern.Umber, BodyPart.Tail)
            ),
            new(5, 
                new(Pattern.Brown, BodyPart.Head),
                new(Pattern.Umber, BodyPart.Head),
                new(Pattern.Spotted, BodyPart.Tail),
                new(Pattern.Grey, BodyPart.Tail)
            ),
            new(6, 
                new(Pattern.Grey, BodyPart.Head),
                new(Pattern.Brown, BodyPart.Head),
                new(Pattern.Spotted, BodyPart.Tail),
                new(Pattern.Umber, BodyPart.Tail)
            ),
            new(7, 
                new(Pattern.Grey, BodyPart.Head),
                new(Pattern.Spotted, BodyPart.Head),
                new(Pattern.Brown, BodyPart.Tail),
                new(Pattern.Umber, BodyPart.Tail)
            ),
            new(8, 
                new(Pattern.Grey, BodyPart.Head),
                new(Pattern.Umber, BodyPart.Head),
                new(Pattern.Brown, BodyPart.Tail),
                new(Pattern.Spotted, BodyPart.Tail)
            ),
            new(9, 
                new(Pattern.Grey, BodyPart.Head),
                new(Pattern.Umber, BodyPart.Head),
                new(Pattern.Grey, BodyPart.Tail),
                new(Pattern.Spotted, BodyPart.Tail)
            )
        ];

        Board = new ([
            [Tiles[0], Tiles[1], Tiles[2]],
            [Tiles[3], Tiles[4], Tiles[5]],
            [Tiles[6], Tiles[7], Tiles[8]],
        ]);
    }
}