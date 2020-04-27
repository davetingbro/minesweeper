using System;

namespace Minesweeper.Exceptions
{
    /// <summary>
    /// Base class for all game logic exceptions
    /// </summary>
    public class GameException : Exception
    {
        protected GameException()
        {
        }

        protected GameException(string message) : base(message)
        {
        }

        protected GameException(string message, Exception inner) :base(message, inner)
        {
        }
    }
}