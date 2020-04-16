using System.Collections.Generic;

namespace Minesweeper
{
    /// <summary>
    /// A data structure for storing a collection of cells in the game
    /// </summary>
    public class GameBoard
    {
        public readonly Dictionary<string, Cell> Cells;
        public readonly int NumOfMines;
        public readonly int Width;
        public readonly int Height;

        public GameBoard(int numOfMines, int width, int height)
        {
            NumOfMines = numOfMines;
            Width = width;
            Height = height;
            Cells = new Dictionary<string, Cell>();
        }
    }
}