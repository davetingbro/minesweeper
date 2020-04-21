using System.Collections.Generic;

namespace Minesweeper.GameActions
{
    public class RevealAction : GameAction
    {
        public override Coordinate Coordinate { get; set; }
        

        public override List<Cell> GetNextBoardState(GameBoard gameBoard)
        {
            throw new System.NotImplementedException();
        }
    }
}