namespace Snake.Logic;

public abstract class Cell
{
    /*
        Empty           = Does absolutely nothing
        Apple           = Randomly Generated Points
        SnakeHead/Head  = Parent of the snake body
        Snake           = Snake body
        SnakeEnd/end    = Where the snake ends

    */
    public int PosX { get; set; }
    public int PosY { get; set; }

    public abstract string Name { get; }
    public abstract string Symbol { get; }
    public abstract int Code { get; }
}
