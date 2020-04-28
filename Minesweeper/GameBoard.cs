using System.Collections.Generic;
using System.Linq;

namespace Minesweeper
{
    /// <summary>
    /// Data structure that represents the GameBoard of the game
    /// </summary>
    public class GameBoard
    {
        public List<Cell> BoardState { get; set; }
        public int Width { get; }
        public int Height { get; }

        public GameBoard(int width, int height)
        {
            Width = width;
            Height = height;
            BoardState = new List<Cell>();
            LoadCells();
        }

        private void LoadCells()
        {
            for (var y = 1; y <= Height; y++)
            {
                for (var x = 1; x <= Width; x++)
                {
                    BoardState.Add(new Cell(x, y));
                }
            }
        }

        public Cell GetCell(Coordinate coordinate)
        {
            return BoardState.Find(c => c.X == coordinate.X && c.Y == coordinate.Y);
        }

        public List<Cell> GetCellNeighbours(Cell cell)
        {
            int x = cell.X, y = cell.Y;
            var currentCell = BoardState.Where(c => c.X == x && c.Y == y);
            var neighbours = BoardState
                .Where(c => c.X >= (x - 1) && c.X <= (x + 1) 
                                           && c.Y >= (y - 1) && c.Y <= (y + 1))
                .Except(currentCell)
                .ToList();

            return neighbours;
        }
    }
}