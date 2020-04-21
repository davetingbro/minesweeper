namespace Minesweeper.Interfaces
{
    /// <summary>
    /// Provides the Render method signature for rendering a game board to display
    /// </summary>
    public interface IGameBoardRenderer
    {
        void Render(GameBoard gameBoard);
    }
}