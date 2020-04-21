using System.Collections.Generic;

namespace Minesweeper.GameActions
{
    public class ActionFlag : GameAction
    {
        public sealed override Coordinate Coordinate { get; set; }

        public ActionFlag(Coordinate coordinate)
        {
            Coordinate = coordinate;
        }

        public override List<Cell> GetNextBoardState(GameBoard gameBoard)
        {
            throw new System.NotImplementedException();
        }
    }
}