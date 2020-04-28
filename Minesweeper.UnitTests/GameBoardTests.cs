using System.Collections.Generic;
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

        [Fact]
        public void ShouldLoadCellsWithCorrectCoordinates()
        {
            var gameBoard = new GameBoard(5, 5);

            var cell = gameBoard.BoardState[1]; 
            var expected = new Cell(2, 1);

            var resultJson = JsonConvert.SerializeObject(cell);
            var expectedJson = JsonConvert.SerializeObject(expected);
            Assert.Equal(expectedJson, resultJson);
        }

        [Theory]
        [InlineData(4, 7)]
        public void ShouldReturnCorrectCellGivenCoordinate(int x, int y)
        {
            var gameBoard = new GameBoard(8, 8);
            var coordinate = new Coordinate(x, y);

            var result = gameBoard.GetCell(coordinate);

            var resultJson = JsonConvert.SerializeObject(result);
            var expectedJson = JsonConvert.SerializeObject(new Cell(x, y));
            Assert.Equal(expectedJson, resultJson);
        }

        [Fact]
        public void ShouldReturnCorrectCellNeighbours()
        {
            var gameBoard = new GameBoard(5, 5);
            var coordinate = new Coordinate(3, 3);
            var cell = gameBoard.GetCell(coordinate);

            var result = gameBoard.GetCellNeighbours(cell);

            var expected = new List<Cell>
            {
                new Cell(2, 2), new Cell(3, 2), new Cell(4, 2),
                new Cell(2, 3),                       new Cell(4, 3),
                new Cell(2, 4), new Cell(3, 4), new Cell(4, 4)
            };

            var resultJson = JsonConvert.SerializeObject(result);
            var expectedJson = JsonConvert.SerializeObject(expected);
            Assert.Equal(expectedJson, resultJson);
        }
    }
}