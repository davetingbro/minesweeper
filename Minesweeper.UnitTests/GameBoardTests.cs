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
            
            var expected = gameBoard.Width * gameBoard.Height;
            
            Assert.Equal(expected, gameBoard.Cells.Count);
        }

        [Fact]
        public void PlantMines_WhenCalled_CanPlantCorrectNumberOfMines()
        {
            var gameBoard = new GameBoard(5, 5, 5);
            gameBoard.LoadCells();

            gameBoard.PlantMines();
            
            var result = gameBoard.Cells.Values.Count(c => c.IsMine);
            var expected = gameBoard.NumOfMines;
            Assert.Equal(expected, result);
        }
    }
}