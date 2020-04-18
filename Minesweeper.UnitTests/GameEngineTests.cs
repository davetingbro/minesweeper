using Moq;
using Xunit;

namespace Minesweeper.UnitTests
{
    public class GameEngineTests
    {
        [Fact]
        public void NumOfPlantMineCallsShouldEqualNumOfMines_WhenInitialize()
        {
            var mockGameBoard = new Mock<GameBoard>(5, 5);
            var gameEngine = new GameEngine(mockGameBoard.Object);
            const int numOfMines = 5;
            
            gameEngine.Initialize(numOfMines);
            
            mockGameBoard.Verify(gb => gb.PlantMine(It.IsAny<int>()), 
                Times.Exactly(numOfMines));
        }
    }
}