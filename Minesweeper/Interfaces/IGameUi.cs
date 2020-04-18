namespace Minesweeper.Interfaces
{
    /// <summary>
    /// User interaction method signatures
    /// </summary>
    public interface IGameUi
    {
        GameBoard GetDimension();
        int GetNumOfMines();
        Action GetUserAction();
        void PrintBoard(GameBoard gameBoard);
        void PrintResult(bool isWon);
    }
}