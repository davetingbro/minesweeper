using Minesweeper.PlayerCommands;

namespace Minesweeper.Interfaces
{
    /// <summary>
    /// User interaction method signatures
    /// </summary>
    public interface IGameUi
    {
        GameBoard GetDimension();
        int GetNumOfMines();
        PlayerCommand GetPlayerCommand();
        void DisplayGameBoard(GameBoard gameBoard);
        void PrintResult(bool isWon);
    }
}