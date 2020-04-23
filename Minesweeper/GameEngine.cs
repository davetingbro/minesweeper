using System;
using Minesweeper.Enums;
using Minesweeper.Interfaces;
using Minesweeper.PlayerCommands;

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

        public void Initialize()
        {
            PlantMines();
            GameBoard.SetAllCellAdjacentMineCount();
        }

        public void PlayUserAction(PlayerCommand action)
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
                var index = new Random().Next(GameBoard.BoardState.Count);
                var cell = GameBoard.BoardState[index];
                var isMinePlanted = cell.IsMine;
                if (isMinePlanted) continue;
                cell.PlantMine();
                numOfMinePlanted++;
            }
        }

        private void UpdateGameState(Coordinate coordinate)
        {
            var cell = GameBoard.GetCell(coordinate);
            
            if (!cell.IsMine) return;
            NumOfMines--;
            IsPlayerWin = cell.CellState == CellState.Flagged && NumOfMines == 0;
            IsGameFinished = cell.CellState == CellState.Revealed || IsPlayerWin;
        }
    }
}