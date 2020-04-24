using System;
using System.Linq;
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
            SetAllCellAdjacentMineCount();
        }

        public void ExecutePlayerCommand(PlayerCommand command)
        {
            command.Execute(GameBoard);
            UpdateGameState(command.Coordinate);
        }

        private void PlantMines()
        {
            var numOfMinePlanted = 0;
            while (numOfMinePlanted < NumOfMines)
            {
                var index = new Random().Next(GameBoard.BoardState.Count);
                var cell = GameBoard.BoardState[index];
                if (cell.IsMine) continue;
                cell.PlantMine();
                numOfMinePlanted++;
            }
        }
        
        private void SetAllCellAdjacentMineCount()
        {
            foreach (var cell in GameBoard.BoardState)
            {
                var neighbours = GameBoard.GetCellNeighbours(cell);
                cell.AdjacentMineCount = neighbours.Count(c => c.IsMine);
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