using System;
using System.Collections.Generic;

namespace Minesweeper
{
    /// <summary>
    /// Object that stores the information of the playing game board
    /// </summary>
    public class GameBoard
    {
        public readonly Dictionary<string, Cell> Cells;
        public int NumOfMines { get; }
        public int Width { get; }
        public int Height { get; }

        public GameBoard(int numOfMines, int width, int height)
        {
            NumOfMines = numOfMines;
            Width = width;
            Height = height;
            Cells = new Dictionary<string, Cell>();
        }

        public void LoadCells()
        {
            for (var x = 1; x <= Width; x++)
            {
                for (var y = 1; y <= Height; y++)
                {
                    var key = $"{x}{y}";
                    var coordinate = new Coordinate(x, y);
                    Cells.Add(key, new Cell(coordinate));
                }
            }
        }

        public void PlantMine(string key)
        {
            Cells[key].PlantMine();
        }

        public bool IsMinePlanted(string key)
        {
            return Cells[key].IsMine;
        }
    }
}