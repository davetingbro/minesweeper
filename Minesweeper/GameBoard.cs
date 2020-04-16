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
        private readonly int _numOfMines;
        private readonly int _width;
        private readonly int _height;

        public GameBoard(int numOfMines, int width, int height)
        {
            _numOfMines = numOfMines;
            _width = width;
            _height = height;
            Cells = new Dictionary<string, Cell>();
        }

        public void LoadCells()
        {
            for (var x = 1; x <= _width; x++)
            {
                for (var y = 1; y <= _height; y++)
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
            while (numOfMinePlanted < _numOfMines)
            {
                var x = random.Next(1, _width + 1);
                var y = random.Next(1, _height + 1);
                var key = $"{x}{y}";
                if (Cells[key].IsMine) continue;
                Cells[key].PlantMine();
                numOfMinePlanted++;
            }
        }
    }
}