namespace Minesweeper.Interfaces
{
    /// <summary>
    /// Defines the method for rendering a GameBoard object to display
    /// </summary>
    public interface IGameBoardRenderer
    {
        void Render(GameBoard gameBoard);
    }
}