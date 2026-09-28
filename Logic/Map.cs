namespace Snake.Logic;

public class Map
{
    public static HeadCell SnakeHead = new();
    public static EndCell SnakeEnd = new();
    public static SnakeCell.SnakeColor bodyColor = SnakeCell.SnakeColor.Light; 

    public int Width { get; set; }
    public int Length { get; set; }
    public Cell[,] Grid = new Cell[0, 0];

    private void Make(int width, int length)
    {
        Length = length;
        Width = width;
        Grid = new Cell[width, length];
    }

    private void Init()
    {
        for (int i = 0; i < Grid.GetLength(0); i++)
        {
            for (int j = 0; j < Grid.GetLength(1); j++)
            {
                Grid[i, j] = new EmptyCell();
            }
        }
    }

    public (bool isApple, AppleCell? apple) CheckIfCellIsApple(int x,int y)
    {
        if (Grid[x,y] is AppleCell apple)
        {
            return (true, apple);
        }
        return (false, null);
    }


    public void Display()
    {
        Console.Clear();
        for (int i = 0; i < Grid.GetLength(0); i++)
        {
            for (int j = 0; j < Grid.GetLength(1); j++)
            {
                Console.Write(Grid[i, j].Symbol);
            }
            Console.Write("\n");
        }
    }

    public void GenerateAppleRandom()
    {
        Random r = new();
        int maxRange1 = Grid.GetLength(0);
        int maxRange2 = Grid.GetLength(1);
        int applePosX = r.Next(0,maxRange1);
        int applePosY = r.Next(0,maxRange2);


        if (applePosX == SnakeHead.PosX && applePosY == SnakeHead.PosY)
        {
            applePosX = r.Next(0,maxRange1);
            applePosY = r.Next(0,maxRange2);            
        }

        Grid[applePosX,applePosY] = new AppleCell();
    }

    void SpawnSnakeHead()
    {
        int startPosX = Grid.GetLength(0) / 2;
        int startPosY = Grid.GetLength(0) / 2;
        Grid[startPosX, startPosY] = SnakeHead;
        SnakeHead.PosX = startPosX;
        SnakeHead.PosY = startPosY;
    }

    public Map(int width, int length)
    {
        this.Make(width, length);
        this.Init();
        this.SpawnSnakeHead();
        this.Display();
    }
}
