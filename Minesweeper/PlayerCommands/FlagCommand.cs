using System.Linq;
using Minesweeper.Enums;
using Minesweeper.Exceptions;

namespace Minesweeper.PlayerCommands
{
    public class FlagCommand : PlayerCommand
    {
        public FlagCommand(Coordinate coordinate) : base(coordinate) {}

        public override void Execute(GameBoard gameBoard)
        {
            var cell = gameBoard.GetCell(Coordinate);
            Validate(gameBoard, cell);
            cell.CellState = cell.CellState == CellState.Flagged ? CellState.Unrevealed : CellState.Flagged;
        }

        private static void Validate(GameBoard gameBoard, Cell cell)
        {
            var mineCount = gameBoard.BoardState.Count(c => c.IsMine);
            var flagCount = gameBoard.BoardState.Count(c => c.CellState == CellState.Flagged);
            if (flagCount == mineCount) 
                throw new InvalidMoveException("Invalid move: You are out of flags.");
            if (cell.CellState == CellState.Revealed)
                throw new InvalidMoveException("Invalid move: Cannot flag cell that is already revealed.");
        }
    }
}