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
                var index = GetRandomIndex();
                if (GameBoard.IsMinePlanted(index)) continue;
                GameBoard.PlantMine(index);
                numOfMineToPlant--;
            }
        }
        
        private int GetRandomIndex()
        {
            var random = new Random();
            
            return random.Next(GameBoard.Cells.Count);
        }

        public void RevealCell(Coordinate coordinate)
        {
            throw new System.NotImplementedException();
        }
    }
}