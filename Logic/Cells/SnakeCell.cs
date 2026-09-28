namespace Snake.Logic;

public sealed class SnakeCell : Cell
{
    public enum SnakeColor { Dark, Light }

    public override string Name => "SNAKE";
    public override string Symbol { get; }
    public override int Code => 3;

    public SnakeCell(bool color)
    {
        Symbol = color == true  
            ? "\u001b[32m█\u001b[0m"
            : "\u001b[92m█\u001b[0m";
    }
}
