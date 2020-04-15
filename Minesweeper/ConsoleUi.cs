using System;
using System.Collections.Generic;
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

        private static GameBoard ParseToGameBoard(string input)
        {
            var dimension = input.Split().Select(int.Parse).ToList();
            if (dimension.Count != 2)
                throw new ArgumentOutOfRangeException();
            if (dimension.Any(d => d < 0))
                throw new FormatException();
            return new GameBoard(dimension[0], dimension[1]);
        }

        public Action GetUserAction()
        {
            throw new System.NotImplementedException();
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