using System;

namespace Minesweeper.Exceptions
{
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