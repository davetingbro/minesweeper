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
            for (var x = 0; x < GameBoard.Width; x++)
            {
                for (var y = 0; y < GameBoard.Height; y++)
                {
                    var key = $"{x}{y}";
                    var coordinate = new Coordinate(x, y);
                    GameBoard.Cells.Add(key, new Cell(coordinate));
                }
            }
        }

        public void RevealCell(Coordinate coordinate)
        {
            throw new System.NotImplementedException();
        }
    }
}