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
        private int _numOfMine;
        public GameBoard GameBoard { get; }
        public bool IsGameFinished { get; private set; }
        public bool IsPlayerWin { get; private set; }

        public GameEngine(GameBoard gameBoard, int numOfMine)
        {
            GameBoard = gameBoard;
            _numOfMine = numOfMine;
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
            while (numOfMinePlanted < _numOfMine)
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
                _numOfMine--;
            if (_numOfMine > 0) return;
            IsGameFinished = true;
            IsPlayerWin = true;
        }
    }
}