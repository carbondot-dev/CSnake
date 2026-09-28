namespace Snake.Logic;

public sealed class EndCell : Cell
{
    public override string Name => "SNAKEEND";
    public override string Symbol => "\x1b[33m█\x1b[0m";
    public override int Code => 4;
}
