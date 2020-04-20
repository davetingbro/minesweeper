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
        public bool IsPlayerWin { get; }

        public GameEngine(GameBoard gameBoard, int numOfMine)
        {
            GameBoard = gameBoard;
            _numOfMine = numOfMine;
        }

        public GameEngine(GameBoard gameBoard)
        {
            GameBoard = gameBoard;
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
            
            return random.Next(GameBoard.Cells.Count);
        }

        public void PlayUserAction(Action action)
        {
            var actionType = action.ActionType;
            var coordinate = action.Coordinate;
            var cell = GameBoard.GetCell(coordinate);

            switch (actionType)
            {
                case ActionType.Flag:
                    cell.Flag();
                    break;
                case ActionType.Reveal:
                    RevealCell(coordinate);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void RevealCell(Coordinate coordinate)
        {
            var cell = GameBoard.GetCell(coordinate);
            if (cell.IsMine)
                IsGameFinished = true;
            cell.Reveal();
        }
    }
}