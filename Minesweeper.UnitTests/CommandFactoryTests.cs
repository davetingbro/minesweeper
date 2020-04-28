using System;
using Minesweeper.Exceptions;
using Minesweeper.PlayerCommands;
using Xunit;

namespace Minesweeper.UnitTests
{
    public class CommandFactoryTests
    {
        [Theory]
        [InlineData("r", typeof(RevealCommand))]
        [InlineData("f", typeof(FlagCommand))]
        public void ShouldReturnCorrectCommandType(string option, Type expected)
        {
            var command = CommandFactory.GetCommand(option, new Coordinate(1, 1));

            Assert.IsType(expected, command);
        }

        [Fact]
        public void ShouldThrowInvalidInputException_WhenGivenInvalidCommandOption()
        {
            Assert.Throws<InvalidInputException>(() => 
                CommandFactory.GetCommand("invalid_command", new Coordinate(1, 1)));
        }
    }
}