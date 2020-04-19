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
            
            Assert.Equal(25, gameBoard.Cells.Count);
        }

        [Fact]
        public void ShouldSetCellIsMineToTrue_WhenPlantMine()
        {
            var gameBoard = new GameBoard(5, 5);

            const int index = 22;
            gameBoard.PlantMine(index);
            
            Assert.True(gameBoard.Cells[index].IsMine);
        }

        [Fact]
        public void ShouldSetCellAdjacentMineCount()
        {
            var gameBoard = new GameBoard(5, 5);
            gameBoard.PlantMine(6);
            gameBoard.PlantMine(16);
            gameBoard.PlantMine(18);

            gameBoard.SetAllCellAdjacentMineCount();

            var result = gameBoard.Cells.Select(c => c.AdjacentMineCount).ToList();
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
    }
}