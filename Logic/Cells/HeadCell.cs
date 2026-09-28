namespace Snake.Logic;

public sealed class HeadCell : Cell
{
    public void ChangeDirection(Direction direction)
    {
        HeadDirection = direction;
    }

    public Player? Player;

    public Direction HeadDirection { get; set; } = Direction.East;

    public override string Symbol => HeadDirection switch
    {
        Direction.North => "\x1b[43m^\x1b[0m",
        Direction.South => "\x1b[43mv\x1b[0m",
        Direction.East  => "\x1b[43m>\x1b[0m",
        Direction.West  => "\x1b[43m<\x1b[0m",
        _ => "\x1b[43m?\x1b[0m"
    };
    
    public override string Name => "SNAKEHEAD";
    public override int Code => 2;
}
