using System;
using System.Collections.Generic;
using System.IO;
using Minesweeper.Exceptions;
using Minesweeper.Interfaces;
using Minesweeper.PlayerCommands;
using Moq;
using Newtonsoft.Json;
using Xunit;

namespace Minesweeper.UnitTests
{
    public class ConsoleUiTests
    {
        [Fact]
        public void ShouldReturnGameBoardWithCorrectWidthAndHeight_WhenGetDimension()
        {
            MockConsoleReadLine("5 5");
            var consoleUiUnderTest = new ConsoleUi();
            
            var gameBoard = consoleUiUnderTest.GetDimension();
            
            var result = JsonConvert.SerializeObject(gameBoard);
            var expectedGameBoard = new GameBoard(5, 5);
            var expected = JsonConvert.SerializeObject(expectedGameBoard);
            
            Assert.Equal(expected, result);
        }
        
        [Theory]
        [InlineData("5 a")]
        [InlineData("7 9.5")]
        [InlineData("")]
        [InlineData("-5 6")]
        [InlineData("55")]
        [InlineData("5 6 7")]
        public void ShouldThrowErrors_WhenReadInvalidInput(string input)
        {
            MockConsoleReadLine(input);
            
            var consoleUi = new ConsoleUi();

            Assert.Throws<InvalidInputException>(consoleUi.GetDimension);
        }

        [Theory]
        [InlineData("r 0 1", typeof(RevealCommand))]
        [InlineData("f 2 4", typeof(FlagCommand))]
        public void ShouldCreateCorrectCommandObject_WhenGivenValidCommandOption(string input, Type expected)
        {
            MockConsoleReadLine(input);
            var consoleUiUnderTest = new ConsoleUi();
            
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
        public void ShouldThrowException_WhenReadInvalidCommandOption(string input)
        {
            MockConsoleReadLine(input);
            
            var consoleUiUnderTest = new ConsoleUi();
            
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
            var mockRenderer = new Mock<IGameBoardRenderer>();
            var consoleUi = new ConsoleUi(mockRenderer.Object);
            
            consoleUi.DisplayGameBoard(stubGameBoard.Object);
            
            mockRenderer.Verify(r => r.Render(stubGameBoard.Object), Times.Once);
        }

        [Theory]
        [InlineData(true, "You win!\n")]
        [InlineData(false, "You lose!\n")]
        public void ShouldPrintCorrectResult(bool isWon, string expected)
        {
            var consoleUi = new ConsoleUi();
            var consoleWriter = new StringWriter();
            Console.SetOut(consoleWriter);
            
            consoleUi.PrintResult(isWon);

            var result = consoleWriter.GetStringBuilder().ToString();
            Assert.Equal(expected, result);
        }
    }
}