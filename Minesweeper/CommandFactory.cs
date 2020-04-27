using Minesweeper.Exceptions;
using Minesweeper.PlayerCommands;

namespace Minesweeper
{
    /// <summary>
    /// Creates PlayerCommand based on user input
    /// </summary>
    public static class CommandFactory
    {
        public static PlayerCommand GetCommand(string commandOption, Coordinate coordinate)
        {
            return commandOption switch
            {
                "r" => new RevealCommand(coordinate),
                "f" => new FlagCommand(coordinate),
                _ => throw new InvalidInputException("Invalid Input: incorrect command option " +
                                                     "(i.e. 'r' - reveal, 'f' - flag/unflag")
            };
        }
    }
}