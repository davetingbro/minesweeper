using Xunit;

namespace Minesweeper.UnitTests
{
    public class CellTests
    {
        [Fact]
        public void ShouldSetIsMineToTrue_WhenPlantMineIsCalled()
        {
            var cell = new Cell(1, 1);
            
            cell.PlantMine();
            
            Assert.True(cell.IsMine);
        }
    }
}