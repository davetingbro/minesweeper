namespace Minesweeper
{
    /// <summary>
    /// A data structure for storing a collection of cells in the game
    /// </summary>
    public class GameBoard
    {
        public Cell[] Cells { get; set; }
        public int Width;
        public int Height;

        public GameBoard(int width, int height)
        {
            Width = width;
            Height = height;
        }
    }
}