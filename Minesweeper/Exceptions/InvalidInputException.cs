using System;

namespace Minesweeper.Exceptions
{
    /// <summary>
    /// The Exception thrown when player enters invalid input
    /// </summary>
    public class InvalidInputException : GameException
    {
        public InvalidInputException(string message) : base(message)
        {
        }
    }
}