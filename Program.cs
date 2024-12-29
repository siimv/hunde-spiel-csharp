// See https://aka.ms/new-console-template for more information

using ConfoundedDogGame.Players;
using ConfoundedDogGame.Visualizers;

var visualizer = new DefaultVisualizer();
// var visualizer = new AsciiVisualizer();

var player = new BruteForcePlayer(Game.Tiles);
var boards = player.Play();

Console.WriteLine($"Total nr of boards: {boards.Count}");

foreach (var board in boards)
{
    Console.WriteLine("----------");
    Console.WriteLine(visualizer.VisualizeBoard(board));
}