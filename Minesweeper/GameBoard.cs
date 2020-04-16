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
        
        public void PlantMines()
        {
            var random = new Random();
            var numOfMinePlanted = 0;
            while (numOfMinePlanted < NumOfMines)
            {
                var x = random.Next(1, Width + 1);
                var y = random.Next(1, Height + 1);
                var key = $"{x}{y}";
                if (Cells[key].IsMine) continue;
                Cells[key].PlantMine();
                numOfMinePlanted++;
            }
        }
    }
}