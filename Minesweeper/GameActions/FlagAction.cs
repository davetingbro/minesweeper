using System.Collections.Generic;
using Minesweeper.Exceptions;

namespace Minesweeper.GameActions
{
    public class FlagAction : GameAction
    {
        public sealed override Coordinate Coordinate { get; set; }

        public FlagAction(Coordinate coordinate)
        {
            Coordinate = coordinate;
        }

        public override List<Cell> GetNextBoardState(GameBoard gameBoard)
        {
            var cell = gameBoard.GetCell(Coordinate);
            if (cell.CellState == CellState.Revealed)
                throw new InvalidMoveException("Invalid move: Cannot flag cell that is already revealed.");
            cell.CellState = CellState.Flagged;
            return gameBoard.BoardState;
        }
    }
}