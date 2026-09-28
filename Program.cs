using System.Data;
using System.Formats.Asn1;
using Snake.Logic;

namespace Snake;
public enum Direction
{
    South,
    East,
    North,
    West
}

public class Program
{
    static async Task Main(string[] args)
    {
        Game game = new(12,25);
        await game.RunAsync();
    }
}
