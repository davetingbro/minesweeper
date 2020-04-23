using System;

namespace Minesweeper.Exceptions
{
    public class InvalidInputException : GameException
    {
        public InvalidInputException()
        {
        }

        public InvalidInputException(string message) : base(message)
        {
        }

        public InvalidInputException(string message, Exception inner) :base(message, inner)
        {
        }
    }
}