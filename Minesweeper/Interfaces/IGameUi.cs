using Minesweeper.PlayerCommands;

namespace Minesweeper.Interfaces
{
    /// <summary>
    /// Defines game UI methods
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