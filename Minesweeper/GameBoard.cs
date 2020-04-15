namespace Minesweeper
{
    /// <summary>
    /// A data structure for storing a collection of cells in the game
    /// </summary>
    public class GameBoard
    {
        public Cell[] Cells;
        public int Width;
        public int Height;

        public GameBoard(Cell[] cells, int width, int height)
        {
            Cells = cells;
            Width = width;
            Height = height;
        }
    }
}