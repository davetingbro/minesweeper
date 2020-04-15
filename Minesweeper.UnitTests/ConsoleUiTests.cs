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
        [InlineData("5, a")]
        [InlineData("9.5 10")]
        [InlineData("-5 6")]
        public void GetDimension_InputContainsNonPositiveIntegers_ThrowsFormatException(string input)
        {
            var sr = new StringReader(input);
            Console.SetIn(sr);

            Assert.Throws<FormatException>(_consoleUiUnderTest.GetDimension);

        }

        [Theory]
        [InlineData("55")]
        [InlineData("5 6 7")]
        public void GetDimension_NumberOfInputNotEqualToTwo_ThrowsArgumentOutOfRangeException(string input)
        {
            var sr = new StringReader(input);
            Console.SetIn(sr);

            Assert.Throws<ArgumentOutOfRangeException>(_consoleUiUnderTest.GetDimension);
        }

        [Fact]
        public void GetDimension_EmptyInput_ThrowsNullReferenceException()
        {
            var sr = new StringReader("");
            Console.SetIn(sr);

            Assert.Throws<NullReferenceException>(_consoleUiUnderTest.GetDimension);
        }
    }
}