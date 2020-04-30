using System;
using System.IO;
using Minesweeper.Exceptions;
using Minesweeper.Interfaces;
using Minesweeper.PlayerCommands;
using Moq;
using Newtonsoft.Json;
using Xunit;

namespace Minesweeper.UnitTests
{
    [Collection("ConsoleUiTestCollection")]
    public class ConsoleUiTests
    {
        private readonly Mock<IGameBoardRenderer> _mockRenderer;

        public ConsoleUiTests()
        {
            _mockRenderer = new Mock<IGameBoardRenderer>();
        }
        
        [Fact]
        public void ShouldReturnGameBoardWithCorrectWidthAndHeight_WhenGivenValidConsoleInput()
        {
            MockConsoleReadLine("5 5");
            var consoleUi = new ConsoleUi(_mockRenderer.Object);
            
            var result = consoleUi.GetDimension();
            
            var resultJson = JsonConvert.SerializeObject(result);
            var expectedJson = JsonConvert.SerializeObject(new GameBoard(5, 5));
            Assert.Equal(expectedJson, resultJson);
        }
        
        [Theory]
        [InlineData("5 a")]
        [InlineData("7 9.5")]
        [InlineData("")]
        [InlineData("-5 6")]
        [InlineData("55")]
        [InlineData("5 6 7")]
        public void ShouldThrowInvalidInputException_WhenGivenInvalidDimensionInput(string input)
        {
            MockConsoleReadLine(input);
            var consoleUi = new ConsoleUi(_mockRenderer.Object);

            Assert.Throws<InvalidInputException>(consoleUi.GetDimension);
        }

        [Fact]
        public void ShouldReturnNumOfMineThatMatchInput()
        {
            MockConsoleReadLine("5");
            var consoleUi = new ConsoleUi(_mockRenderer.Object);

            var result = consoleUi.GetNumOfMines();
            
            Assert.Equal(5, result);
        }

        [Theory]
        [InlineData("")]
        [InlineData("0")]
        [InlineData("-5")]
        [InlineData("a")]
        public void ShowThrowInvalidInputException_WhenGivenInvalidNumOfMineInput(string input)
        {
            MockConsoleReadLine(input);
            var consoleUi = new ConsoleUi(_mockRenderer.Object);

            Assert.Throws<InvalidInputException>(() => consoleUi.GetNumOfMines());
        }

        [Theory]
        [InlineData("r 0 1", typeof(RevealCommand))]
        [InlineData("f 2 4", typeof(FlagCommand))]
        public void ShouldCreateCorrectCommandObject_WhenGivenValidCommandOption(string input, Type expected)
        {
            MockConsoleReadLine(input);
            var consoleUiUnderTest = new ConsoleUi(_mockRenderer.Object);
            
            var action = consoleUiUnderTest.GetPlayerCommand();
            
            Assert.IsType(expected, action);
        }

        [Theory]
        [InlineData("")]
        [InlineData("r 5 a")]
        [InlineData("f 9.5 10")]
        [InlineData("r -5 6")]
        [InlineData("r 2")]
        [InlineData("r 9 10 1")]
        [InlineData("v 9 10")]
        public void ShouldThrowInvalidInputException_WhenReadInvalidCommandOption(string input)
        {
            MockConsoleReadLine(input);
            
            var consoleUiUnderTest = new ConsoleUi(_mockRenderer.Object);
            
            Assert.Throws<InvalidInputException>(consoleUiUnderTest.GetPlayerCommand);
        }

        private static void MockConsoleReadLine(string input)
        {
            var sr = new StringReader(input);
            Console.Clear();
            Console.SetIn(sr);
        }

        [Fact]
        public void ShouldCallRenderMethod_WhenDisplayGameBoard()
        {
            var stubGameBoard = new Mock<GameBoard>(5, 5);
            var consoleUi = new ConsoleUi(_mockRenderer.Object);
            
            consoleUi.DisplayGameBoard(stubGameBoard.Object);
            
            _mockRenderer.Verify(r => r.Render(stubGameBoard.Object), Times.Once);
        }

        [Theory]
        [InlineData(true, "You win!\n")]
        [InlineData(false, "You lose!\n")]
        public void ShouldPrintCorrectResult(bool isWon, string expected)
        {
            var consoleUi = new ConsoleUi(_mockRenderer.Object);
            var consoleWriter = new StringWriter();
            Console.SetOut(consoleWriter);
            
            consoleUi.PrintResult(isWon);
            var result = consoleWriter.GetStringBuilder().ToString();
            
            Assert.Equal(expected, result);
        }
    }
}