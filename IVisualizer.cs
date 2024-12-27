namespace ConfoundedDogGame;

public interface IVisualizer
{
    string Visualize(Card card);
}

public class DefaultVisualizer : IVisualizer
{
    public string Visualize(Card card)
    {
        return card.ToString();
    }
}