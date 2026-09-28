
using System.Reflection;
using System.Threading.Channels;

namespace Snake.Logic;

public class Player
{
    private HeadCell Head { get; }

    public int PosX => Head.PosX;
    public int PosY => Head.PosY;
    public Direction Direction => Head.HeadDirection;
    public List<Cell> Body = [];

    public int Score { get; set; }
    private Map Map { get; }
    private bool NextSnakeColor = false;

    public void UpdatePos()
    {
        for (int i = 0; i < Map.Grid.GetLength(0); i++)
        {
            for (int j = 0; j < Map.Grid.GetLength(1); j++)
            {
                if (Map.Grid[i,j] is HeadCell head)
                {
                    Head.PosX = i;         
                    Head.PosY = j;         
                }
            }
        }
    }

    private static (int dX, int dY) GetOffsetPos(Direction dir) => dir switch
    {
        Direction.North => (-1, 0),
        Direction.South => (+1, 0),
        Direction.West  => (0, -1),
        Direction.East  => (0, +1),
        _ => (0, 0)
    };

    public void Move()
    {
        try
        {
            var (dRow, dCol) = GetOffsetPos(Head.HeadDirection);

            int newX = Head.PosX + dRow;
            int newY = Head.PosY + dCol;

            var (isApple,apple) = Map.CheckIfCellIsApple(newX, newY);

            if (isApple)
            {
                Score += apple!.Points;
                Map.GenerateAppleRandom();
                Grow();
            }

            Map.Grid[newX, newY] = Head;
            Map.Grid[Head.PosX, Head.PosY] = new EmptyCell();
            
            MoveParts();

            Head.PosX = newX;
            Head.PosY = newY;
            Map.Display();
        } 
        catch (Exception e)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(e);
        }
    }
    private void MoveParts()
    {
        if (Body.Count <= 1) return;

        // this is the ONLY cell that truly becomes vacant
        int tailOldX = Body[Body.Count - 1].PosX;
        int tailOldY = Body[Body.Count - 1].PosY;

        for (int i = Body.Count - 1; i > 0; i--)
        {
            Body[i].PosX = Body[i - 1].PosX;
            Body[i].PosY = Body[i - 1].PosY;
            Map.Grid[Body[i].PosX, Body[i].PosY] = Body[i];   // just overwrite, no erase needed
        }

        Map.Grid[tailOldX, tailOldY] = new EmptyCell();   // clean up the one real gap, once, at the end
    }

    private Direction GetOpposingDirection(Direction dir)
    {
        if (dir == Direction.South) return Direction.North;
        if (dir == Direction.North) return Direction.South;
        if (dir == Direction.West) return Direction.East;
        if (dir == Direction.East) return Direction.West;
        return Direction.North;
    }


    public void Grow()
    {
        var direction = GetOpposingDirection(Direction);

        int newX, newY;

        if (Body.Count == 0)
        {
            newX = PosX;
            newY = PosY;
        }
        else
        {
            newX = Body[Body.Count-1].PosX;
            newY = Body[Body.Count-1].PosY; 
        }
        
        switch (direction)
        {
            case Direction.South:
                newX += 1;
                break; 

            case Direction.North:
                newX -= 1;
                break; 

            case Direction.West:
                newY -= 1;
                break; 

            case Direction.East:
                newY += 1;
                break; 
        }

        SnakeCell newPart = GetNewPart();
        newPart.PosX = newX;
        newPart.PosY = newY;

        Map.Grid[newX, newY] = newPart;
        Body.Add(newPart);
    }

    public SnakeCell GetNewPart()
    {
        var newSnake = new SnakeCell(NextSnakeColor);
        if (NextSnakeColor)
        {
            NextSnakeColor = false;
            return newSnake;
        }
        NextSnakeColor = true;
        return newSnake;
    }

    public void ChangeDirection(ConsoleKeyInfo keyInfo)
    {
        switch (keyInfo.Key)
        {
            case ConsoleKey.W:
                Head.ChangeDirection(Direction.North);
                break;
            case ConsoleKey.A:
                Head.ChangeDirection(Direction.West);
                break;
            case ConsoleKey.S:
                Head.ChangeDirection(Direction.South);
                break;
            case ConsoleKey.D:
                Head.ChangeDirection(Direction.East);
                break;
        }        
    }

    public Player(HeadCell head, Map Map)
    {
        this.Score = 0;
        this.Head = head;
        this.Map = Map;
        Body.Add(Head);
    }
}
