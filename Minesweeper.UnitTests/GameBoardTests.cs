using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Xunit;

namespace Minesweeper.UnitTests
{
    public class GameBoardTests
    {
        [Fact]
        public void ShouldCreateSameNumberOfCellsAsAreaOfGameBoard_WhenLoadCells()
        {
            var gameBoard = new GameBoard(5, 5);
            
            gameBoard.LoadCells();
            
            Assert.Equal(25, gameBoard.Cells.Count);
        }

        [Fact]
        public void ShouldSetCellIsMineToTrue_WhenPlantMine()
        {
            var gameBoard = new GameBoard(5, 5);
            gameBoard.LoadCells();

            const int index = 22;
            gameBoard.PlantMine(index);
            
            Assert.True(gameBoard.Cells[index].IsMine);
        }

        [Fact]
        public void ShouldSetCellAdjacentMineCount()
        {
            var gameBoard = new GameBoard(5, 5);
            gameBoard.LoadCells();
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
    }
}