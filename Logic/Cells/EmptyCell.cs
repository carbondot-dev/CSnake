namespace Snake.Logic;

public sealed class EmptyCell : Cell
{
    public override string Name => "EMPTY";
    public override string Symbol => "\x1b[30m█\x1b[0m";
    public override int Code => 0;
}
