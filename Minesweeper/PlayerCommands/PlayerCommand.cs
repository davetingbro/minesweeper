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

        public abstract void Execute(GameBoard gameBoard);
    }
}