using Minesweeper.Interfaces;

namespace Minesweeper
{
    /// <summary>
    /// Provides implementation of the game logic methods
    /// </summary>
    public class GameEngine : IGameEngine
    {
        public GameBoard GameBoard { get; }
        public bool IsGameFinished { get; }
        public bool IsPlayerWin { get; }

        public GameEngine(GameBoard gameBoard)
        {
            GameBoard = gameBoard;
        }

        public void Initialize()
        {
            GameBoard.LoadCells();
            GameBoard.PlantMines();
        }

        public void RevealCell(Coordinate coordinate)
        {
            throw new System.NotImplementedException();
        }
    }
}