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
        public void PlantMines_WhenCalled_CanPlantCorrectNumberOfMines()
        {
            var gameBoard = new GameBoard(5, 5, 5);
            gameBoard.LoadCells();

            gameBoard.PlantMines();
            
            var result = gameBoard.Cells.Values.Count(c => c.IsMine);
            Assert.Equal(5, result);
        }
    }
}