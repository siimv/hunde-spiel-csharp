namespace ConfoundedDogGame;

public static class Game
{
    public static Card[] Tiles { get; }

    static Game()
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
    }
}