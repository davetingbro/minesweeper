using System.Collections.Generic;

namespace Minesweeper.GameActions
{
    public abstract class GameAction
    {
        public abstract Coordinate Coordinate { get; set; }
        public abstract List<Cell> GetNextBoardState(GameBoard gameBoard);
    }
}