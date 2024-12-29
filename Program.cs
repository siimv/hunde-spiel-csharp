// See https://aka.ms/new-console-template for more information

using ConfoundedDogGame;
using ConfoundedDogGame.Players;
using ConfoundedDogGame.Visualizers;

var visualizer = new DefaultVisualizer();
// var visualizer = new AsciiVisualizer();

var random = new Random();
IEnumerable<Card>[] tiles =
[
    Game.Tiles,
    Game.Tiles.OrderBy(x => x.Number),
    Game.Tiles.OrderBy(x => x.Number).Select(x => x.Rotate()),
    Game.Tiles.OrderBy(x => x.Number).Select(x => x.Rotate().Rotate()),
    Game.Tiles.OrderBy(x => x.Number).Select(x => x.Rotate().Rotate().Rotate()),
    Game.Tiles.OrderBy(x => x.Number).Select(x => x.Rotate().Rotate().Rotate().Rotate()),
    Game.Tiles.OrderBy(x => x.Number).Select(x => x.Rotate().Rotate().Rotate().Rotate().Rotate()),
    Game.Tiles.OrderByDescending(x => x.Number),
    Game.Tiles.OrderByDescending(x => x.Number).Select(x => x.Rotate()),
    Game.Tiles.OrderByDescending(x => x.Number).Select(x => x.Rotate().Rotate()),
    Game.Tiles.OrderBy(x => random.Next()).Select(x => x.Rotate()),
    Game.Tiles.OrderBy(x => random.Next()).Select(x => x.Rotate().Rotate())
];

var player = new BruteForcePlayer();

foreach (var tile in tiles)
{
    var boards = player.Play(tile)
        .Where(x => x.IsComplete())
        .Where(x => x.IsCorrect())
        .ToArray();

    Console.WriteLine($"Total nr of boards: {boards.Length}");
}

// foreach (var board in boards)
// {
//     Console.WriteLine("----------");
//     Console.WriteLine(visualizer.VisualizeBoard(board));
// }