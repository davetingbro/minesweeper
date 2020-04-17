using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Xunit;

namespace Minesweeper.UnitTests
{
    public class ConsoleUiTests
    {
        [Fact]
        public void ShouldReturnGameBoardWithCorrectWidthAndHeight_WhenGetDimension()
        {
            MockConsoleReadLine("5 5 5");
            var consoleUiUnderTest = new ConsoleUi();
            
            var gameBoard = consoleUiUnderTest.GetDimension();
            
            var result = JsonConvert.SerializeObject(gameBoard);
            var expectedGameBoard = new GameBoard(5,5, 5);
            var expected = JsonConvert.SerializeObject(expectedGameBoard);
            
            Assert.Equal(expected, result);
        }
        
        [Theory]
        [InlineData("5 a 4", typeof(FormatException))]
        [InlineData("7 9.5 10", typeof(FormatException))]
        [InlineData("-5 6 8", typeof(FormatException))]
        [InlineData("55", typeof(ArgumentOutOfRangeException))]
        [InlineData("5 6 7 2", typeof(ArgumentOutOfRangeException))]
        [InlineData("", typeof(NullReferenceException))]
        public void ShouldThrowErrors_WhenReadInvalidInput(string input, Type expectedException)
        {
            MockConsoleReadLine(input);
            
            var consoleUi = new ConsoleUi();

            Assert.Throws(expectedException, consoleUi.GetDimension);
        }

        [Theory]
        [MemberData(nameof(GetUserActionValidInputData))]
        public void ShouldCreateCorrectActionObject_WhenGetUserAction(string input, Action expectedAction)
        {
            MockConsoleReadLine(input);
            var consoleUiUnderTest = new ConsoleUi();
            
            var action = consoleUiUnderTest.GetUserAction();

            var result = JsonConvert.SerializeObject(action);
            var expected = JsonConvert.SerializeObject(expectedAction);
            
            Assert.Equal(expected, result);
        }
        
        public static IEnumerable<object[]> GetUserActionValidInputData => new List<object[]>
        {
            new object[] {"r 0 1", new Action(ActionType.Reveal, new Coordinate(0, 1))},
            new object[] {"f 2 4", new Action(ActionType.Flag, new Coordinate(2, 4))},
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

    }
}