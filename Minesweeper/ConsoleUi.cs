using System;
using System.Linq;
using Minesweeper.Exceptions;
using Minesweeper.Interfaces;
using Minesweeper.PlayerCommands;

namespace Minesweeper
{
    /// <summary>
    /// Provides implementations of the user interaction methods
    /// </summary>
    public class ConsoleUi : IGameUi
    {
        private readonly IGameBoardRenderer _gameBoardRenderer;

        public ConsoleUi() { }

        public ConsoleUi(IGameBoardRenderer gameBoardRenderer)
        {
            _gameBoardRenderer = gameBoardRenderer;
        }

        public GameBoard GetDimension()
        {
            Console.Write("Please enter the width and height (e.g. 5 5): ");
            try
            {
                var input = Console.ReadLine();
                return ParseToGameBoard(input);
            }
            catch (FormatException)
            {
                throw new InvalidInputException("Invalid Input: must be two positive integers (e.g. 5 5)");
            }
            catch (NullReferenceException)
            {
                throw new InvalidInputException("Invalid Input: input cannot be empty (e.g. 5 5)");
            }
        }

        private static GameBoard ParseToGameBoard(string input)
        {
            var values = input.Split().Select(int.Parse).ToList();
            if (values.Count != 2 || values.Any(d => d < 0))
                throw new InvalidInputException("Invalid Input: must be two positive integers (e.g. 5 5)");
            return new GameBoard(values[0], values[1]);
        }

        public int GetNumOfMines()
        {
            Console.Write("Please enter the number of mines: ");
            try
            {
                var input = Console.ReadLine();
                if (input == "")
                    throw new NullReferenceException();
                return int.Parse(input);
            }
            catch (FormatException)
            {
                throw new InvalidInputException("Invalid Input: must be positive integers (e.g. 5)");
            }
            catch (NullReferenceException)
            {
                throw new InvalidInputException("Invalid Input: input cannot be empty (e.g. 5)");
            }
        }

        public PlayerCommand GetPlayerCommand()
        {
            Console.Write("Command ('r'/'f') and coordinate (e.g. 2 3): ");
            try
            {
                var input = Console.ReadLine()?.Split();
                return ParseToPlayerCommand(input);
            }
            catch (NullReferenceException)
            {
                throw new InvalidInputException("Invalid Input: input cannot be empty (e.g. r 2 2)");
            }
            catch (FormatException)
            {
                throw new InvalidInputException("Invalid Input: Coordinate value must be positive integers (e.g. r 2 2)");
            }
        }

        private static PlayerCommand ParseToPlayerCommand(string[] input)
        {
            if (input.Length != 3)
                throw new InvalidInputException("Invalid Inputs: must contain 3 values (e.g. r 2 2)");
            
            int x = int.Parse(input[1]), y = int.Parse(input[2]);
            if (x < 0 || y < 0)
                throw new InvalidInputException("Invalid Input: coordinate values must be positive integers (e.g. r 2 2)");
            var coordinate = new Coordinate(x, y);

            var actionInput = input[0].ToLower();
            if (actionInput != "r" && actionInput != "f")
                throw new InvalidInputException("Invalid Input: incorrect command input " +
                                                "(i.e. 'r' - reveal, 'f' - flag/unflag");
            var action = actionInput == "r" ? (PlayerCommand) new RevealCommand(coordinate) : new FlagCommand(coordinate);

            return action;
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