using System;
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

    }
}