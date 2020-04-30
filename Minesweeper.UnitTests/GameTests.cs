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
            MockSetStartingGameState();
        }
        
        private void MockSetStartingGameState()
        {
            _mockGameUi.Setup(ui => ui.GetDimension()).Returns(new GameBoard(5, 5));
            _mockGameUi.Setup(ui => ui.GetNumOfMines()).Returns(5);
        }

        [Fact]
        public void ShouldGetDimensionAndNumOfMinesOnce_WhenRunGame()
        {
            MockSetupGameEngineSequenceFinishGameInFourMoves();

            _game.Run();
                        
            _mockGameUi.Verify(ui => ui.GetDimension(), Times.Once);
            _mockGameUi.Verify(ui => ui.GetNumOfMines(), Times.Once);
        }

        [Fact]
        public void ShouldCallGameEngineInitializeOnce_WhenRunGame()
        {
            MockSetupGameEngineSequenceFinishGameInFourMoves();

            _game.Run();
            
            _mockGameEngine.Verify(ge => ge.Initialize(), Times.Once);
        }

        [Fact]
        public void ShouldContinueToPlayGameUntilIsGameFinishedIsTrue()
        {
            MockSetupGameEngineSequenceFinishGameInFourMoves();

            _game.Run();
            
            _mockGameUi.Verify(ui => ui.GetPlayerCommand(), Times.Exactly(4));
            _mockGameEngine.Verify(ge => ge.ExecutePlayerCommand(It.IsAny<PlayerCommand>()),
                Times.Exactly(4));
        }

        [Fact]
        public void ShouldPrintExceptionMessage_WhenGetDimensionThrowsInvalidInputException()
        {
            MockGetDimensionThrowsInvalidInputException();
            MockSetupGameEngineSequenceFinishGameInFourMoves();
            var consoleWriteLineReader = ConsoleWriteLineReader();

            _game.Run();

            var result = consoleWriteLineReader.GetStringBuilder().ToString();
            Assert.Equal("invalid input\n", result);
        }

        private void MockGetDimensionThrowsInvalidInputException()
        {
            _mockGameUi.SetupSequence(ui => ui.GetDimension())
                .Returns(() => throw new InvalidInputException("invalid input"))
                .Returns(new GameBoard(5, 5));
        }

        [Fact]
        public void ShouldPrintExceptionMessage_WhenGameExceptionIsThrownDuringGamePlay()
        {
            _mockGameUi.Setup(ui => ui.GetPlayerCommand())
                .Callback(() => throw new InvalidInputException("invalid input"));
            _mockGameEngine.SetupSequence(ge => ge.IsGameFinished)
                .Returns(false)
                .Returns(true);
            var consoleWriteLineReader = ConsoleWriteLineReader();
            
            _game.Run();

            var result = consoleWriteLineReader.GetStringBuilder().ToString();
            Assert.Equal("invalid input\n", result);
        }

        private static StringWriter ConsoleWriteLineReader()
        {
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            return stringWriter;
        }

        [Fact]
        public void ShouldDisplayGameBoardDuringGameRun()
        {
            MockSetupGameEngineSequenceFinishGameInFourMoves();

            _game.Run();
            
            _mockGameUi.Verify(ui => ui.DisplayGameBoard(It.IsAny<GameBoard>()), Times.Exactly(5));
        }

        [Fact]
        public void ShouldPrintResultWhenGameIsFinished()
        {
            MockSetupGameEngineSequenceFinishGameInFourMoves();

            _game.Run();
            
            _mockGameUi.Verify(ui => ui.PrintResult(It.IsAny<bool>()),
                Times.Once);
        }

        private void MockSetupGameEngineSequenceFinishGameInFourMoves()
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