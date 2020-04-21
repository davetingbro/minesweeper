using System;
using Minesweeper.GameActions;
using Minesweeper.Interfaces;

namespace Minesweeper
{
    /// <summary>
    /// Provides implementation of the game logic methods
    /// </summary>
    public class GameEngine : IGameEngine
    {
        public GameBoard GameBoard { get; set; }
        public int NumOfMines { get; set; }
        public bool IsGameFinished { get; private set; }
        public bool IsPlayerWin { get; private set; }

        public GameEngine(GameBoard gameBoard, int numOfMines)
        {
            GameBoard = gameBoard;
            NumOfMines = numOfMines;
        }

        public void Initialize()
        {
            PlantMines();
            GameBoard.SetAllCellAdjacentMineCount();
        }

        public void PlayUserAction(GameAction action)
        {
            var nextBoardState = action.GetNextBoardState(GameBoard);
            GameBoard.BoardState = nextBoardState;
            UpdateGameState(action.Coordinate);
        }

        private void PlantMines()
        {
            var numOfMinePlanted = 0;
            while (numOfMinePlanted < NumOfMines)
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
            
            return random.Next(GameBoard.BoardState.Count);
        }

        private void UpdateGameState(Coordinate coordinate)
        {
            var cell = GameBoard.GetCell(coordinate);
            if (cell.IsMine)
                NumOfMines--;
            if (NumOfMines > 0) return;
            IsGameFinished = true;
            IsPlayerWin = true;
        }
    }
}