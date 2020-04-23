using System.Collections.Generic;

namespace Minesweeper.PlayerCommands
{
    public abstract class PlayerCommand
    {
        public readonly Coordinate Coordinate;

        protected PlayerCommand(Coordinate coordinate)
        {
            Coordinate = coordinate;
        }

        public abstract List<Cell> GetNextBoardState(GameBoard gameBoard);

        public abstract void Execute(GameBoard gameBoard);
    }
}