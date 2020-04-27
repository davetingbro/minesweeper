using System.Collections.Generic;

namespace Minesweeper.PlayerCommands
{
    /// <summary>
    /// The base class for all player commands - defines the Execute method
    /// </summary>
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