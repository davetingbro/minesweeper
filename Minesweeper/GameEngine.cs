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
            GameBoard.LoadCells();
        }

        private void PlantMines()
        {
            var numOfMineToPlant = GameBoard.NumOfMines;
            while (numOfMineToPlant > 0)
            {
                var key = GetRandomCellKey();
                if (GameBoard.IsMinePlanted(key)) continue;
                GameBoard.PlantMine(key);
                numOfMineToPlant--;
            }
        }
        
        private string GetRandomCellKey()
        {
            var random = new Random();
            var x = random.Next(1, GameBoard.Width + 1);
            var y = random.Next(1, GameBoard.Height + 1);
            
            return $"{x}{y}";
        }

        public void RevealCell(Coordinate coordinate)
        {
            throw new System.NotImplementedException();
        }
    }
}