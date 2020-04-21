using Minesweeper.Interfaces;
using Moq;
using Xunit;

namespace Minesweeper.UnitTests
{
    public class GameTests
    {
        private readonly Game _game;
        private readonly Mock<IGameEngine> _mockGameEngine;
        private readonly Mock<IGameUi> _mockGameUi;
        
        public GameTests()
        {
            _mockGameEngine = new Mock<IGameEngine>();
            _mockGameUi = new Mock<IGameUi>();
            _game = new Game(_mockGameEngine.Object, _mockGameUi.Object);
        }

        [Fact]
        public void ShouldInitializeGameCorrectly()
        {
            _game.Play();
                        
            _mockGameUi.Verify(ui => ui.GetDimension(), Times.Once);
            _mockGameUi.Verify(ui => ui.GetNumOfMines(), Times.Once);
            _mockGameEngine.Verify(ge => ge.Initialize(), Times.Once);
        }
    }
}