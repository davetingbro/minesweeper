using System;
using System.Linq.Expressions;
using Minesweeper.Interfaces;
using Minesweeper.PlayerCommands;
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
            SetupMockGameEngine();
            _mockGameEngine.SetupGet(ge => ge.IsGameFinished).Returns(true);

            _game.Run();
                        
            _mockGameUi.Verify(ui => ui.GetDimension(), Times.Once);
            _mockGameUi.Verify(ui => ui.GetNumOfMines(), Times.Once);
            _mockGameEngine.Verify(ge => ge.Initialize(), Times.Once);
        }

        [Fact]
        public void ShouldContinueToPlayGameUntilIsGameFinishedIsTrue()
        {
            SetupMockGameEngine();
            _mockGameEngine
                .SetupSequence(ge => ge.IsGameFinished)
                .Returns(false)
                .Returns(false)
                .Returns(true);

            _game.Run();
            
            _mockGameUi.Verify(ui => ui.GetPlayerCommand(), Times.Exactly(2));
            _mockGameEngine.Verify(ge => ge.ExecutePlayerCommand(It.IsAny<PlayerCommand>()),
                Times.Exactly(2));
            _mockGameUi.Verify(ui => ui.PrintResult(It.IsAny<bool>()),
                Times.Once);
        }

        [Fact]
        public void ShouldDisplayGameBoardDuringGameRun()
        {
            SetupMockGameEngine();
            _mockGameEngine
                .SetupSequence(ge => ge.IsGameFinished)
                .Returns(false)
                .Returns(false)
                .Returns(true);

            _game.Run();
            
            _mockGameUi.Verify(ui => ui.DisplayGameBoard(It.IsAny<GameBoard>()), Times.Exactly(3));
        }

        private void SetupMockGameEngine()
        {
            _mockGameEngine.SetupSequence(ge => ge.GameBoard)
                .Returns(value: null)
                .Returns(value: null)
                .Returns(new GameBoard(5, 5));
            _mockGameEngine.Setup(ge => ge.NumOfMines)
                .Returns(5);
        }
    }
}