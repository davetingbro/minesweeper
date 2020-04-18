using System.Collections.Generic;
using System.Linq;

namespace Minesweeper
{
    /// <summary>
    /// Object that stores the information of the playing game board
    /// </summary>
    public class GameBoard
    {
        public readonly List<Cell> Cells = new List<Cell>();
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
                    Cells.Add(new Cell(coordinate));
                }
            }
        }

        public virtual void PlantMine(int index)
        {
            Cells[index].PlantMine();
        }

        public bool IsMinePlanted(int index)
        {
            return Cells[index].IsMine;
        }

        public virtual void SetAllCellAdjacentMineCount()
        {
            foreach (var cell in Cells)
            {
                var cellAdjacentMineCount = GetAdjacentMineCount(cell);
                cell.AdjacentMineCount = cellAdjacentMineCount;
            }
        }

        private int GetAdjacentMineCount(Cell cell)
        {
            int x = cell.X, y = cell.Y;

            var currentCell = Cells.Where(c => c.X == x && c.Y == y);
            var neighbours = Cells
                    .Where(c => c.X >= (x - 1) && c.X <= (x + 1) 
                                   && c.Y >= (y - 1) && c.Y <= (y + 1))
                    .Except(currentCell);

            return neighbours.Count(n => n.IsMine);
        }
    }
}