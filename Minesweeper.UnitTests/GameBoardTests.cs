using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Xunit;

namespace Minesweeper.UnitTests
{
    public class GameBoardTests
    {
        [Fact]
        public void ShouldCreateSameNumberOfCellsAsAreaOfGameBoard_WhenInstantiated()
        {
            var gameBoard = new GameBoard(5, 5);
            
            Assert.Equal(25, gameBoard.BoardState.Count);
        }

        [Theory]
        [InlineData(4, 7)]
        public void ShouldReturnCorrectCellGivenCoordinate(int x, int y)
        {
            var gameBoard = new GameBoard(8, 8);
            var coordinate = new Coordinate(x, y);

            var cell = gameBoard.GetCell(coordinate);
            var expectedCell = new Cell(coordinate);

            var result = JsonConvert.SerializeObject(cell);
            var expected = JsonConvert.SerializeObject(expectedCell);
            Assert.Equal(expected, result);
        }
    }
}