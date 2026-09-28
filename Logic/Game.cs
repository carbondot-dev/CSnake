using Microsoft.VisualBasic;
using Snake;
using System.Data;
using System.Linq.Expressions;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Snake.Logic;

public class NoPlayerException : Exception
{
    public override string Message => "No Player Found";
}

public class Game
{
    protected Map map = null!;
    Player Player { get; set;}
    volatile bool running = true;

    public async Task RunAsync()
    {
        var gameLoop = GameLoopAsync();
        var inputHandler = ListenForInputAsync();

        await Task.WhenAll(gameLoop, inputHandler);
    }

    private async Task ListenForInputAsync()
    {
        while (running)
        {
            if (Console.KeyAvailable)
            {
                var keyInfo = Console.ReadKey(true);
                Player.ChangeDirection(keyInfo);
            }
            await Task.Delay(10);
        }
    }

    private async Task GameLoopAsync()
    {
        while (true)
        {            
            Player.UpdatePos();
            Player.Move();
            Console.WriteLine($"Player location = ({Player.PosX},{Player.PosY})"+
                            $"\nDirection:{Player.Direction}"+
                            $"\nScore: {Player.Score}");
            await Task.Delay(150);
        }
    }

    public Game(int mapWidth, int mapLength)
    {
        map = new(mapWidth,mapLength);
        Player = new(Map.SnakeHead, map);
        Player.UpdatePos();
        map.GenerateAppleRandom();
    }
}