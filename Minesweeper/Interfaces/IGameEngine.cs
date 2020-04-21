using Minesweeper.GameActions;

namespace Minesweeper.Interfaces
{
    /// <summary>
    /// Game logic method signatures
    /// </summary>
    public interface IGameEngine
    {
        GameBoard GameBoard { get; set; }
        int NumOfMines { get; set; }
        bool IsGameFinished { get; }
        bool IsPlayerWin { get; }

        void Initialize();
        void PlayUserAction(GameAction action);
    }
}