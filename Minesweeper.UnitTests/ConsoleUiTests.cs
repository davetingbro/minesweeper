using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Xunit;

namespace Minesweeper.UnitTests
{
    public class ConsoleUiTests
    {
        private readonly ConsoleUi _consoleUiUnderTest;

        public ConsoleUiTests()
        {
            _consoleUiUnderTest = new ConsoleUi();
        }
        
        [Fact]
        public void GetDimension_ValidInput_ReturnsGameBoardWithCorrectWidthHeight()
        {
            var sr = new StringReader("5 5");
            Console.SetIn(sr);

            var gameBoard = _consoleUiUnderTest.GetDimension();
            var expectedGameBoard = new GameBoard(5, 5);

            var result = JsonConvert.SerializeObject(gameBoard);
            var expected = JsonConvert.SerializeObject(expectedGameBoard);
            
            Assert.Equal(expected, result);
        }
        
        [Theory]
        [InlineData("5, a", typeof(FormatException))]
        [InlineData("9.5 10", typeof(FormatException))]
        [InlineData("-5 6", typeof(FormatException))]
        [InlineData("55", typeof(ArgumentOutOfRangeException))]
        [InlineData("5 6 7", typeof(ArgumentOutOfRangeException))]
        [InlineData("", typeof(NullReferenceException))]
        public void GetDimension_InvalidInput_TypeOfExceptionThrownMatchesExpected(string input, Type expectedException)
        {
            var sr = new StringReader(input);
            Console.SetIn(sr);

            Assert.Throws(expectedException, _consoleUiUnderTest.GetDimension);
        }

        [Theory]
        [MemberData(nameof(GetUserActionValidInputData))]
        public void GetUserAction_ValidInput_ReturnsCorrectActionObject(string input, Action expectedAction)
        {
            var sr = new StringReader(input);
            Console.SetIn(sr);

            var action = _consoleUiUnderTest.GetUserAction();

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
        public void GetUserAction_InvalidInput_TypeOfExceptionThrownMatchesExpected(string input, Type expectedException)
        {
            var sr = new StringReader(input);
            Console.SetIn(sr);

            Assert.Throws(expectedException, _consoleUiUnderTest.GetUserAction);
        }
        


    }
}