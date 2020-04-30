using System;
using System.Linq;
using Minesweeper.Exceptions;
using Minesweeper.Interfaces;
using Minesweeper.PlayerCommands;

namespace Minesweeper
{
    /// <summary>
    /// Provides game UI methods implementation for console interface
    /// </summary>
    public class ConsoleUi : IGameUi
    {
        private readonly IGameBoardRenderer _gameBoardRenderer;
        
        public ConsoleUi(IGameBoardRenderer gameBoardRenderer)
        {
            _gameBoardRenderer = gameBoardRenderer;
        }

        public GameBoard GetDimension()
        {
            Console.Write("Please enter the width and height (e.g. 5 5): ");
            var input = Console.ReadLine();
            return ParseToGameBoard(input);
        }

        private static GameBoard ParseToGameBoard(string input)
        {
            const string errorMessage = "Invalid Input: please enter two positive integers (e.g. 5 5)";
            try
            {
                var values = input.Split().Select(int.Parse).ToList();
                if (values.Count != 2 || values.Any(d => d < 0))
                    throw new InvalidInputException(errorMessage);
                return new GameBoard(values[0], values[1]);
            }
            catch (FormatException)
            {
                throw new InvalidInputException(errorMessage);
            }
            catch (NullReferenceException)
            {
                throw new InvalidInputException(errorMessage);
            }
        }

        public int GetNumOfMines()
        {
            Console.Write("Please enter the number of mines: ");
            
            const string errorMessage = "Invalid Input: please enter a positive integers (e.g. 5)";
            try
            {
                var input = Console.ReadLine();
                var value = int.Parse(input ?? throw new InvalidInputException(errorMessage));
                return value > 0 ? value : throw new InvalidInputException(errorMessage);
            }
            catch (FormatException)
            {
                throw new InvalidInputException(errorMessage);
            }
        }

        public PlayerCommand GetPlayerCommand()
        {
            Console.Write("Command ('r'/'f') and coordinate (e.g. 2 3): ");
            var input = Console.ReadLine()?.Split();
            return ParseToPlayerCommand(input);
        }

        private static PlayerCommand ParseToPlayerCommand(string[] input)
        {
            ValidatePlayerCommand(input);
            var commandOption = input[0].ToLower();
            int x = int.Parse(input[1]), y = int.Parse(input[2]);
            var coordinate = new Coordinate(x, y);

            return CommandFactory(commandOption, coordinate);
        }

        private static void ValidatePlayerCommand(string[] input)
        {
            try
            {
                if (input.Length != 3)
                    throw new InvalidInputException("Invalid Inputs: must contain 3 values (e.g. r 2 2)");
                int x = int.Parse(input[1]), y = int.Parse(input[2]);
                if (x < 0 || y < 0)
                    throw new InvalidInputException("Invalid Input: coordinate values must be positive integers (e.g. r 2 2)");
            }
            catch (FormatException)
            {
                throw new InvalidInputException(
                    "Invalid Input: coordinate value must be positive integers (e.g. r 2 2)");
            }
            catch (NullReferenceException)
            {
                throw new InvalidInputException("Invalid Input: input cannot be empty (e.g. r 2 2)");
            }
        }

        private static PlayerCommand CommandFactory(string commandOption, Coordinate coordinate)
        {
            return commandOption switch
            {
                "r" => new RevealCommand(coordinate),
                "f" => new FlagCommand(coordinate),
                _ => throw new InvalidInputException("Invalid Input: incorrect command option " +
                                                     "(i.e. 'r' - reveal, 'f' - flag/unflag")
            };
        }

        public void DisplayGameBoard(GameBoard gameBoard)
        {
            _gameBoardRenderer.Render(gameBoard);
        }

        public void PrintResult(bool isWon)
        {
            Console.WriteLine(isWon ? "You win!" : "You lose!");
        }
    }
}