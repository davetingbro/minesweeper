using Minesweeper.Enums;
using Xunit;
using Moq;

namespace Minesweeper.UnitTests
{
    public class CellTests
    {
        [Fact]
        public void ShouldSetIsMineToTrue_WhenPlantMineIsCalled()
        {
            var cell = GetCellUnderTest();
            
            cell.PlantMine();
            
            Assert.True(cell.IsMine);
        }
        
        [Fact]
        public void ShouldSetCellStateToRevealed_WhenRevealIsCalled()
        {
            var cell = GetCellUnderTest();

            cell.CellState = CellState.Revealed;

            var result = cell.CellState;
            const CellState expected = CellState.Revealed;
            Assert.Equal(expected, result);
        }

        [Fact]
        public void ShouldSetCellStateToFlagged_WhenFlagIsCalled()
        {
            var cell = GetCellUnderTest();

            cell.CellState = CellState.Flagged;

            var result = cell.CellState;
            const CellState expected = CellState.Flagged;
            Assert.Equal(expected, result);
        }

        private static Cell GetCellUnderTest()
        {
            var fakeCoordinate = new Mock<Coordinate>(1, 1);
            return new Cell(fakeCoordinate.Object);
        }
    }
}