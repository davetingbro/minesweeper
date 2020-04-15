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
            throw new System.NotImplementedException();
        }

        public Coordinate GetCoordinateFromInput()
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