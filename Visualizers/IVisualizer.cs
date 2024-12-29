namespace ConfoundedDogGame.Visualizers;

public interface IVisualizer
{
    string Visualize(Card card);

    string VisualizeBoard(Board board);
}