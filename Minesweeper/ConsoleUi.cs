using System;
using System.Linq;
using Minesweeper.Interfaces;

namespace Minesweeper
{
    /// <summary>
    /// Provides implementations of the user interaction methods
    /// </summary>
    public class ConsoleUi : IGameUi
    {
        public GameBoard GetDimension()
        {
            try
            {
                var input = Console.ReadLine();
                return ParseToGameBoard(input);
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("Error: Input must contain two positive integers (e.g. 5 5)");
                throw;
            }
            catch (FormatException)
            {
                Console.WriteLine("Error: Input must be positive whole numbers (e.g. 5 5)");
                throw;
            }
            catch (NullReferenceException)
            {
                Console.WriteLine("Error: Input cannot be empty (e.g. 5 5)");
                throw;
            }
        }

        public int GetNumOfMines()
        {
            try
            {
                var input = Console.ReadLine();
                if (input == "")
                    throw new NullReferenceException();
                return int.Parse(input);
            }
            catch (FormatException)
            {
                Console.WriteLine("Error: Input must be positive whole number (e.g. 5)");
                throw;
            }
            catch (NullReferenceException)
            {
                Console.WriteLine("Error: Input cannot be empty (e.g. 5)");
                throw;
            }
        }

        private static GameBoard ParseToGameBoard(string input)
        {
            var values = input.Split().Select(int.Parse).ToList();
            if (values.Count != 2)
                throw new ArgumentOutOfRangeException();
            if (values.Any(d => d < 0))
                throw new FormatException();
            return new GameBoard(values[0], values[1]);
        }

        public Action GetUserAction()
        {
            try
            {
                var input = Console.ReadLine()?.Split();
                return ParseToAction(input);
            }
            catch (NullReferenceException)
            {
                Console.WriteLine("Error: Input cannot be empty (e.g. r 2 2)");
                throw;
            }
            catch (FormatException)
            {
                Console.WriteLine("Error: Coordinate value must be positive whole numbers (e.g. r 2 2)");
                throw;
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("Error: Input must contain 3 values (e.g. r 2 2)");
                throw;
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Error: Incorrect action input (i.e. 'r' - reveal, 'f' - flag");
                throw;
            }
        }

        private static Action ParseToAction(string[] input)
        {
            if (input.Length != 3)
                throw new ArgumentOutOfRangeException();
            
            var actionInput = input[0].ToLower();
            if (actionInput != "r" && actionInput != "f")
                throw new ArgumentException();
            var actionType = actionInput == "r" ? ActionType.Reveal : ActionType.Flag;
            
            int x = int.Parse(input[1]), y = int.Parse(input[2]);
            if (x < 0 || y < 0)
                throw new FormatException();

            return new Action(actionType, new Coordinate(x, y));
        }

        public void PrintBoard(GameBoard gameBoard)
        {
            throw new System.NotImplementedException();
        }

        public void PrintResult(bool isWon)
        {
            throw new System.NotImplementedException();
        }
    }
}