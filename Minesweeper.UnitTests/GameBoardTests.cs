using System.Linq;
using Xunit;

namespace Minesweeper.UnitTests
{
    public class GameBoardTests
    {
        [Fact]
        public void LoadCells_WhenCalled_CellArrayHoldsSameNumberOfCellsAsAreaOfGameBoard()
        {
            var gameBoard = new GameBoard(5, 5, 5);
            
            gameBoard.LoadCells();
            
            Assert.Equal(25, gameBoard.Cells.Count);
        }

        [Fact]
        public void PlantMine_WhenCalled_CanSetCellIsMineToTrue()
        {
            var gameBoard = new GameBoard(5, 5, 5);
            gameBoard.LoadCells();

            const string key = "22";
            gameBoard.PlantMine(key);
            
            Assert.True(gameBoard.Cells[key].IsMine);
        }
    }
}