using System;

namespace Minesweeper.Exceptions
{
    /// <summary>
    /// The Exception thrown when user performs an invalid move
    /// </summary>
    public class InvalidMoveException : GameException
    {
        public InvalidMoveException(string message) : base(message)
        {
        }
    }
}