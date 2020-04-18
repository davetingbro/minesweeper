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

        public void Initialize(int numOfMines)
        {
            GameBoard.LoadCells();
        }

        private void PlantMines(int numOfMine)
        {
            var numOfMinePlanted = 0;
            while (numOfMinePlanted < numOfMine)
            {
                var index = GetRandomIndex();
                if (GameBoard.IsMinePlanted(index)) continue;
                GameBoard.PlantMine(index);
                numOfMinePlanted++;
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