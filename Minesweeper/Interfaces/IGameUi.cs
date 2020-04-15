namespace Minesweeper.Interfaces
{
    /// <summary>
    /// User interaction method signatures
    /// </summary>
    public interface IGameUi
    {
        GameBoard GetDimension();
        Coordinate GetCoordinateFromInput();
        void PrintBoard(GameBoard gameBoard);
        void PrintResult(bool isWon);
    }
}