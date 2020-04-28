using System;
using System.Linq;
using Minesweeper.Enums;
using Minesweeper.Exceptions;
using Minesweeper.Interfaces;
using Minesweeper.PlayerCommands;

namespace Minesweeper
{
    /// <summary>
    /// Provides game logic implementation to advance game state
    /// </summary>
    public class GameEngine : IGameEngine
    {
        private int _numOfMines;
        public int NumOfMines
        {
            get => _numOfMines;
            set =>
                _numOfMines = value <= GameBoard.Width * GameBoard.Height
                    ? value
                    : throw new InvalidInputException("Invalid Input: cannot have more mines than board size");
        }
        public GameBoard GameBoard { get; set; }
        public bool IsGameFinished { get; private set; }
        public bool IsPlayerWin { get; private set; }

        public void Initialize()
        {
            PlantMines();
            SetAllCellAdjacentMineCount();
        }

        private void PlantMines()
        {
            var numOfMinePlanted = 0;
            while (numOfMinePlanted < _numOfMines)
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

        public void ExecutePlayerCommand(PlayerCommand command)
        {
            var coordinate = command.Coordinate;
            if (coordinate.X > GameBoard.Width || coordinate.Y > GameBoard.Height)
                throw new InvalidMoveException("Invalid Move: Coordinate out of range.");
            command.Execute(GameBoard);
            UpdateGameState(command.Coordinate);
        }

        private void UpdateGameState(Coordinate coordinate)
        {
            var cell = GameBoard.GetCell(coordinate);
            
            if (!cell.IsMine) return;
            _numOfMines--;
            IsPlayerWin = _numOfMines == 0 && cell.CellState == CellState.Flagged;
            IsGameFinished = cell.CellState == CellState.Revealed || IsPlayerWin;
        }
    }
}