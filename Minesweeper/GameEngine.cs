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
            PlantMines(numOfMines);
            GameBoard.SetAllCellAdjacentMineCount();
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
                    cell.Reveal();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}