﻿// See https://aka.ms/new-console-template for more information

using ConfoundedDogGame;
using ConfoundedDogGame.Players;
using ConfoundedDogGame.Visualizers;

var random = new Random();
// Try out different ordering, each one of them should produce the same result
IEnumerable<Card>[] shuffledCardDecks =
[
    Game.Tiles,
    Game.Tiles.OrderBy(x => x.Number),
    Game.Tiles.OrderBy(x => x.Number).Select(x => x.Rotations().ElementAt(1)),
    Game.Tiles.OrderBy(x => x.Number).Select(x => x.Rotations().ElementAt(2)),
    Game.Tiles.OrderBy(x => x.Number).Select(x => x.Rotations().ElementAt(3)),
    Game.Tiles.OrderByDescending(x => x.Number),
    Game.Tiles.OrderByDescending(x => x.Number).Select(x => x.Rotations().ElementAt(1)),
    Game.Tiles.OrderByDescending(x => x.Number).Select(x => x.Rotations().ElementAt(2)),
    Game.Tiles.OrderBy(x => random.Next()).Select(x => x.Rotations().ElementAt(1)),
    Game.Tiles.OrderBy(x => random.Next()).Select(x => x.Rotations().ElementAt(2))
];

var visualizer = new SimpleVisualizer();
// var visualizer = new AsciiVisualizer();

//TODO: Add filters for visually different cards
foreach (var cardDeck in shuffledCardDecks)
{
    var player = new BranchBasedPlayer(cardDeck);
    var boards = player.Play()
        .Aggregate(new
        {
            TotalNrOfBoards = 0,
            NrOfCompletedBoards = 0,
            CorrectBoards = new List<Board>()
        }, (agg, b) =>
        {
            var isCompleted = 0;
            if (b.IsCorrect())
            {
                isCompleted = 1;
                agg.CorrectBoards.Add(b);
            }
            else if (b.IsComplete())
            {
                isCompleted = 1;
            }

            return agg with
            {
                TotalNrOfBoards = agg.TotalNrOfBoards + 1,
                NrOfCompletedBoards = agg.NrOfCompletedBoards + isCompleted,
            };
        });

    Console.WriteLine("----------");
    Console.WriteLine($"Total nr of boards: {boards.TotalNrOfBoards}");
    Console.WriteLine($"Nr of complete boards: {boards.NrOfCompletedBoards}");
    Console.WriteLine($"Nr of final (solution) boards: {boards.CorrectBoards.Count}");

    // foreach (var board in boards.CorrectBoards)
    // {
    //     Console.WriteLine("----------");
    //     Console.WriteLine(visualizer.VisualizeBoard(board));
    // }
}