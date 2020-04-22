using System.Collections.Generic;

namespace Minesweeper.GameActions
{
    public class UnflagAction : GameAction
    {
        public UnflagAction(Coordinate coordinate) : base(coordinate)
        {
        }

        public override List<Cell> GetNextBoardState(GameBoard gameBoard)
        {
            throw new System.NotImplementedException();
        }
    }
}