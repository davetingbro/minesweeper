using System.Collections.Generic;
using System.Linq;

namespace Minesweeper
{
    /// <summary>
    /// Object that stores the information of the playing game board
    /// </summary>
    public class GameBoard
    {
        public List<Cell> BoardState { get; set; } = new List<Cell>();
        public int Width { get; }
        public int Height { get; }

        public GameBoard(int width, int height)
        {
            Width = width;
            Height = height;
            LoadCells();
        }

        private void LoadCells()
        {
            for (var y = 1; y <= Height; y++)
            {
                for (var x = 1; x <= Width; x++)
                {
                    var coordinate = new Coordinate(x, y);
                    BoardState.Add(new Cell(coordinate));
                }
            }
        }

        public virtual void SetAllCellAdjacentMineCount()
        {
            foreach (var cell in BoardState)
            {
                var neighbours = GetCellNeighbours(cell);
                cell.AdjacentMineCount = neighbours.Count(c => c.IsMine);
            }
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

        public Cell GetCell(Coordinate coordinate)
        {
            return BoardState.Find(c => c.X == coordinate.X && c.Y == coordinate.Y);
        }
    }
}