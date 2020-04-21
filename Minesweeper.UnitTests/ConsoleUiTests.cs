using System;
using System.Collections.Generic;
using System.IO;
using Minesweeper.GameActions;
using Minesweeper.Interfaces;
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
        [InlineData("5 a", typeof(FormatException))]
        [InlineData("7 9.5", typeof(FormatException))]
        [InlineData("-5 6", typeof(FormatException))]
        [InlineData("55", typeof(ArgumentOutOfRangeException))]
        [InlineData("5 6 7", typeof(ArgumentOutOfRangeException))]
        [InlineData("", typeof(NullReferenceException))]
        public void ShouldThrowErrors_WhenReadInvalidInput(string input, Type expectedException)
        {
            MockConsoleReadLine(input);
            
            var consoleUi = new ConsoleUi();

            Assert.Throws(expectedException, consoleUi.GetDimension);
        }

        [Theory]
        [MemberData(nameof(GetUserActionValidInputData))]
        public void ShouldCreateCorrectActionObject_WhenGetUserAction(string input, Type expected)
        {
            MockConsoleReadLine(input);
            var consoleUiUnderTest = new ConsoleUi();
            
            var action = consoleUiUnderTest.GetUserAction();
            
            Assert.IsType(expected, action);
        }
        
        public static IEnumerable<object[]> GetUserActionValidInputData => new List<object[]>
        {
            new object[] {"r 0 1", typeof(RevealAction)},
            new object[] {"f 2 4", typeof(FlagAction)},
        };

        [Theory]
        [InlineData("", typeof(NullReferenceException))]
        [InlineData("r 5 a", typeof(FormatException))]
        [InlineData("f 9.5 10", typeof(FormatException))]
        [InlineData("r -5 6", typeof(FormatException))]
        [InlineData("r 2", typeof(ArgumentOutOfRangeException))]
        [InlineData("r 9 10 1", typeof(ArgumentOutOfRangeException))]
        [InlineData("v 9 10", typeof(ArgumentException))]
        public void ShouldThrowException_WhenReadInvalidActionInput(string input, Type expectedException)
        {
            MockConsoleReadLine(input);
            
            var consoleUiUnderTest = new ConsoleUi();
            
            Assert.Throws(expectedException, consoleUiUnderTest.GetUserAction);
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