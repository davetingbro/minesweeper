using Minesweeper.PlayerCommands;

namespace Minesweeper.Interfaces
{
    /// <summary>
    /// Defines the Game state and methods that update them
    /// </summary>
    public interface IGameEngine
    {
        GameBoard GameBoard { get; set; }
        int NumOfMines { get; set; }
        bool IsGameFinished { get; }
        bool IsPlayerWin { get; }

        void Initialize();
        void ExecutePlayerCommand(PlayerCommand command);
    }
}