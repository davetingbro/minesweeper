namespace Minesweeper.Interfaces
{
    /// <summary>
    /// Game logic method signatures
    /// </summary>
    public interface IGameEngine
    {
        GameBoard GameBoard { get; }
        bool IsGameFinished { get; }
        bool IsPlayerWin { get; }

        void Initialize();
        void RevealCell(Coordinate coordinate);
    }
}