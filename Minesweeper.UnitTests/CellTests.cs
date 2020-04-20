using Minesweeper.Exceptions;
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

            cell.Reveal();

            var result = cell.CellState;
            const CellState expected = CellState.Revealed;
            Assert.Equal(expected, result);
        }

        [Fact]
        public void ShouldSetCellStateToFlagged_WhenFlagIsCalled()
        {
            var cell = GetCellUnderTest();

            cell.Flag();

            var result = cell.CellState;
            const CellState expected = CellState.Flagged;
            Assert.Equal(expected, result);
        }

        [Fact]
        public void ShouldThrowInvalidMoveException_WhenFlaggingRevealedCell()
        {
            var cell = GetCellUnderTest();
            cell.Reveal();

            Assert.Throws<InvalidMoveException>(cell.Flag);
        }

        private static Cell GetCellUnderTest()
        {
            var fakeCoordinate = new Mock<Coordinate>(1, 1);
            return new Cell(fakeCoordinate.Object);
        }

        [Theory]
        [InlineData(CellState.Unrevealed, 0, false, "-")]
        [InlineData(CellState.Flagged, 0, true, "F")]
        [InlineData(CellState.Revealed, 2, false, "2")]
        [InlineData(CellState.Revealed, 0, true, "*")]
        public void ShouldConvertCellToStringThatMatchesExpected(CellState cellState, int adjacentMineCount, bool isMine, 
            string expected)
        {
            var cell = new Cell(cellState, adjacentMineCount, isMine);

            var result = cell.ToString();
            
            Assert.Equal(expected, result);
        }
    }
}