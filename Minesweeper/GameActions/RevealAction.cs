using System.Collections.Generic;

namespace Minesweeper.GameActions
{
    public class RevealAction : GameAction
    {
        public sealed override Coordinate Coordinate { get; set; }

        public RevealAction(Coordinate coordinate)
        {
            Coordinate = coordinate;
        }

        public override List<Cell> GetNextBoardState(GameBoard gameBoard)
        {
            var cell = gameBoard.GetCell(Coordinate);
            cell.CellState = CellState.Revealed;
            return gameBoard.BoardState;
        }
    }
}