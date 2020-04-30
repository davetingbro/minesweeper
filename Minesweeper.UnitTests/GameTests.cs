using System;
using System.IO;
using Minesweeper.Exceptions;
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
            MockSetGameState();
        }
        
        private void MockSetGameState()
        {
            _mockGameEngine.SetupSequence(ge => ge.GameBoard)
                .Returns(value: null)
                .Returns(value: null)
                .Returns(new GameBoard(5, 5));
            _mockGameEngine.Setup(ge => ge.NumOfMines)
                .Returns(5);
        }

        [Fact]
        public void ShouldGetDimensionAndNumOfMinesOnce_WhenRunGame()
        {
            SetupGameEngineSequenceFinishGameInFourMoves();

            _game.Run();
                        
            _mockGameUi.Verify(ui => ui.GetDimension(), Times.Once);
            _mockGameUi.Verify(ui => ui.GetNumOfMines(), Times.Once);
        }
        
        [Fact]
        public void ShouldPrintExceptionMessage_WhenGetDimensionThrowsInvalidInputException()
        {
            SetupGameEngineSequenceFinishGameInFourMoves();
            var consoleWriter = new StringWriter();
            Console.SetOut(consoleWriter);
            _mockGameUi.Setup(ui => ui.GetDimension())
                .Callback(() => throw new InvalidInputException("invalid input"));

            _game.Run();

            var result = consoleWriter.GetStringBuilder().ToString();
            Assert.Equal("invalid input\n", result);
        }
        
        [Fact]
        public void ShouldCallGameEngineInitializeOnce_WhenRunGame()
        {
            SetupGameEngineSequenceFinishGameInFourMoves();

            _game.Run();
            
            _mockGameEngine.Verify(ge => ge.Initialize(), Times.Once);
        }

        [Fact]
        public void ShouldContinueToPlayGameUntilIsGameFinishedIsTrue()
        {
            SetupGameEngineSequenceFinishGameInFourMoves();

            _game.Run();
            
            _mockGameUi.Verify(ui => ui.GetPlayerCommand(), Times.Exactly(4));
            _mockGameEngine.Verify(ge => ge.ExecutePlayerCommand(It.IsAny<PlayerCommand>()),
                Times.Exactly(4));
        }

        [Fact]
        public void ShouldPrintExceptionMessage_WhenGameExceptionIsThrownDuringGamePlay()
        {
            _mockGameEngine.SetupSequence(ge => ge.IsGameFinished)
                .Returns(false)
                .Returns(true);
            _mockGameUi.Setup(ui => ui.GetPlayerCommand())
                .Callback(() => throw new InvalidInputException("invalid input"));
            var consoleWriter = new StringWriter();
            Console.SetOut(consoleWriter);
            
            _game.Run();

            var result = consoleWriter.GetStringBuilder().ToString();
            Assert.Equal("invalid input\n", result);
        }

        [Fact]
        public void ShouldDisplayGameBoardDuringGameRun()
        {
            SetupGameEngineSequenceFinishGameInFourMoves();

            _game.Run();
            
            _mockGameUi.Verify(ui => ui.DisplayGameBoard(It.IsAny<GameBoard>()), Times.Exactly(5));
        }

        [Fact]
        public void ShouldPrintResultWhenGameIsFinished()
        {
            SetupGameEngineSequenceFinishGameInFourMoves();

            _game.Run();
            
            _mockGameUi.Verify(ui => ui.PrintResult(It.IsAny<bool>()),
                Times.Once);
        }

        private void SetupGameEngineSequenceFinishGameInFourMoves()
        {
            _mockGameEngine
                .SetupSequence(ge => ge.IsGameFinished)
                .Returns(false)
                .Returns(false)
                .Returns(false)
                .Returns(false)
                .Returns(true);
        }
    }
}