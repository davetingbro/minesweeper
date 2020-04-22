using System.Collections.Generic;
using Minesweeper.Enums;

namespace Minesweeper.GameActions
{
    public class UnflagAction : GameAction
    {
        public UnflagAction(Coordinate coordinate) : base(coordinate)
        {
        }

        public override List<Cell> GetNextBoardState(GameBoard gameBoard)
        {
            var cell = gameBoard.GetCell(Coordinate);
            cell.CellState = CellState.Unrevealed;
            return gameBoard.BoardState;
        }
    }
}