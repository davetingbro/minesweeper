using System.Collections.Generic;
using Minesweeper.Enums;

namespace Minesweeper.GameActions
{
    public class RevealAction : GameAction
    {
        public RevealAction(Coordinate coordinate) : base(coordinate) {}

        public override List<Cell> GetNextBoardState(GameBoard gameBoard)
        {
            var cell = gameBoard.GetCell(Coordinate);
            cell.CellState = CellState.Revealed;
            return gameBoard.BoardState;
        }
    }
}