using Minesweeper.GameActions;

namespace Minesweeper.Interfaces
{
    /// <summary>
    /// User interaction method signatures
    /// </summary>
    public interface IGameUi
    {
        GameBoard GetDimension();
        int GetNumOfMines();
        GameAction GetUserAction();
        void DisplayGameBoard(GameBoard gameBoard);
        void PrintResult(bool isWon);
    }
}