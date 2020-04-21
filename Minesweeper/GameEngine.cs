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
        
        // Todo: delete this method 
        public void PlayUserAction(Action action)
        {
            throw new NotImplementedException();
        }

        private void UpdateGameState(bool isMine)
        {
            if (isMine)
                _numOfMine--;
            if (_numOfMine > 0) return;
            IsGameFinished = true;
            IsPlayerWin = true;
        }
    }
}