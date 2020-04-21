using System.Collections.Generic;

namespace Minesweeper.GameActions
{
    public class ActionReveal : GameAction
    {
        public override Coordinate Coordinate { get; set; }
        

        public override List<Cell> GetNextBoardState(GameBoard gameBoard)
        {
            throw new System.NotImplementedException();
        }
    }
}