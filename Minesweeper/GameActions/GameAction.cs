using System.Collections.Generic;

namespace Minesweeper.GameActions
{
    public abstract class GameAction
    {
        public readonly Coordinate Coordinate;

        protected GameAction(Coordinate coordinate)
        {
            Coordinate = coordinate;
        }

        public abstract List<Cell> GetNextBoardState(GameBoard gameBoard);
    }
}