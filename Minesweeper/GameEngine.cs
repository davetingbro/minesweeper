using System;
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

        public void PlayUserAction(Action action)
        {
            var actionType = action.ActionType;
            var coordinate = action.Coordinate;

            switch (actionType)
            {
                case ActionType.Flag:
                    FlagCell(coordinate);
                    break;
                case ActionType.Reveal:
                    RevealCell(coordinate);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void FlagCell(Coordinate coordinate)
        {
            var cell = GameBoard.GetCell(coordinate);
            cell.Flag();
            UpdateGameState(cell.IsMine);
        }

        private void UpdateGameState(bool isMine)
        {
            if (isMine)
                _numOfMine--;
            if (_numOfMine > 0) return;
            IsGameFinished = true;
            IsPlayerWin = true;
        }

        private void RevealCell(Coordinate coordinate)
        {
            var cell = GameBoard.GetCell(coordinate);
            cell.Reveal();
            if (cell.IsMine)
                IsGameFinished = true;
        }
    }
}