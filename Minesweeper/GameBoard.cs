using System.Collections.Generic;

namespace Minesweeper
{
    /// <summary>
    /// Object that stores the information of the playing game board
    /// </summary>
    public class GameBoard
    {
        public readonly List<Cell> Cells;
        public int NumOfMines { get; }
        public int Width { get; }
        public int Height { get; }

        public GameBoard(int numOfMines, int width, int height)
        {
            NumOfMines = numOfMines;
            Width = width;
            Height = height;
            Cells = new List<Cell>();
        }

        public void LoadCells()
        {
            for (var x = 1; x <= Width; x++)
            {
                for (var y = 1; y <= Height; y++)
                {
                    var coordinate = new Coordinate(x, y);
                    Cells.Add(new Cell(coordinate));
                }
            }
        }

        public void PlantMine(int index)
        {
            Cells[index].PlantMine();
        }

        public bool IsMinePlanted(int index)
        {
            return Cells[index].IsMine;
        }
    }
}