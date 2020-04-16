using System.Linq;
using Xunit;

namespace Minesweeper.UnitTests
{
    public class GameEngineTests
    {
        [Fact]
        public void Initialize_WhenCalled_CellArrayHoldsSameNumberOfCellsAsAreaOfGameBoard()
        {
            var fakeGameBoard = new GameBoard(5, 5, 5);
            var gameEngine = new GameEngine(fakeGameBoard);
            gameEngine.Initialize();

            var gameBoard = gameEngine.GameBoard;
            var expected = gameBoard.Width * gameBoard.Height;
            
            Assert.Equal(expected, gameBoard.Cells.Count);
        }

        [Fact]
        public void Initialize_WhenCalled_CellArrayContainsCorrectNumberOfMines()
        {
            var fakeGameBoard = new GameBoard(5, 5, 5);
            var gameEngine = new GameEngine(fakeGameBoard);
            gameEngine.Initialize();

            var gameBoard = gameEngine.GameBoard;
            var result = gameBoard.Cells.Count(c => c.IsMine);
            var expected = gameBoard.NumOfMines;
            
            Assert.Equal(expected, result);
        }
    }
}