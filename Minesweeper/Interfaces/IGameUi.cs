namespace Minesweeper.Interfaces
{
    /// <summary>
    /// User interaction method signatures
    /// </summary>
    public interface IGameUi
    {
        GameBoard GetDimension();
        Action GetUserAction();
        void PrintBoard(GameBoard gameBoard);
        void PrintResult(bool isWon);
    }
}