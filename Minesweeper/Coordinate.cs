namespace Minesweeper
{
    /// <summary>
    /// Data structure that represents the coordinate information of each GameBoard cell
    /// </summary>
    public class Coordinate
    {
        public readonly int X;
        public readonly int Y;

        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}