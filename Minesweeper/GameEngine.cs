using System;
using Minesweeper.Interfaces;

namespace Minesweeper
{
    /// <summary>
    /// Provides implementation of the game logic methods
    /// </summary>
    public class GameEngine : IGameEngine
    {
        public GameBoard GameBoard { get; }
        public bool IsGameFinished { get; }
        public bool IsPlayerWin { get; }

        public GameEngine(GameBoard gameBoard)
        {
            GameBoard = gameBoard;
        }

        public void Initialize()
        {
            LoadCells();
            PlantMines();
        }

        private void LoadCells()
        {
            for (var x = 1; x <= GameBoard.Width; x++)
            {
                for (var y = 1; y <= GameBoard.Height; y++)
                {
                    var key = $"{x}{y}";
                    var coordinate = new Coordinate(x, y);
                    GameBoard.Cells.Add(key, new Cell(coordinate));
                }
            }
        }

        private void PlantMines()
        {
            var random = new Random();
            var numOfMine = GameBoard.NumOfMines;
            while (numOfMine > 0)
            {
                var x = random.Next(1, GameBoard.Width + 1);
                var y = random.Next(1, GameBoard.Height + 1);
                var key = $"{x}{y}";
                if (GameBoard.Cells[key].IsMine) continue;
                GameBoard.Cells[key].IsMine = true;
                numOfMine--;
            }
        }

        public void RevealCell(Coordinate coordinate)
        {
            throw new System.NotImplementedException();
        }
    }
}