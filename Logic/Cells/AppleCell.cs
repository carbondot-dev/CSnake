namespace Snake.Logic;

public sealed class AppleCell : Cell
{
    public override string Name => "APPLE";
    public override string Symbol => "\x1b[31m█\x1b[0m";
    public override int Code => 1;

    public int Points { get; set; } = 1;
}
