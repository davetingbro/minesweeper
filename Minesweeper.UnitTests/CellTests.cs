using Xunit;
using Moq;

namespace Minesweeper.UnitTests
{
    public class CellTests
    {
        [Fact]
        public void ShouldSetCellStateToRevealed_WhenRevealIsCalled()
        {
            var fakeCoordinate = new Mock<Coordinate>(1, 1);
            var cell = new Cell(fakeCoordinate.Object);

            cell.Reveal();

            var result = cell.CellState;
            var expected = CellState.Revealed;
            Assert.Equal(expected, result);
        }
    }
}