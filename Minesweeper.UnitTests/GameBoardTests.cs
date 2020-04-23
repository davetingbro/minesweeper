using System.Collections.Generic;
using System.Linq;
using Minesweeper.PlayerCommands;
using Moq;
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
        public void ShouldSetCellAdjacentMineCount()
        {
            var gameBoard = new GameBoard(5, 5);
            gameBoard.BoardState[6].PlantMine();
            gameBoard.BoardState[16].PlantMine();
            gameBoard.BoardState[18].PlantMine();

            gameBoard.SetAllCellAdjacentMineCount();

            var result = gameBoard.BoardState.Select(c => c.AdjacentMineCount).ToList();
            var expected = new List<int>
            {
                1, 1, 1, 0, 0,
                1, 0, 1, 0, 0,
                2, 2, 3, 1, 1,
                1, 0, 2, 0, 1,
                1, 1, 2, 1, 1,
            };

            var resultJson = JsonConvert.SerializeObject(result);
            var expectedJson = JsonConvert.SerializeObject(expected);
            
            Assert.Equal(expectedJson, resultJson);
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

        [Fact]
        public void ShouldCallPlayerCommandExecuteMethod_WhenExecuteCommand()
        {
            var gameBoard = new GameBoard(5, 5);
            var mockCommand = new Mock<PlayerCommand>(new Coordinate(1, 1));

            gameBoard.ExecuteCommand(mockCommand.Object);
            
            mockCommand.Verify(c => c.Execute(gameBoard.BoardState), Times.Once);
        }
    }
}