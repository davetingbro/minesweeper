using System;
using System.IO;
using Xunit;

namespace Minesweeper.UnitTests
{
    public class GameBoardRendererTests
    {
        [Fact]
        public void ShouldRenderGameBoardAsExpected()
        {
            var gameBoard = new GameBoard(5, 5);
            var renderer = new GameBoardRenderer();
            var consoleWriter = new StringWriter();
            Console.SetOut(consoleWriter);
            
            renderer.Render(gameBoard);

            var result = consoleWriter.GetStringBuilder().ToString();
            const string expected = " - - - - -\n" +
                                    " - - - - -\n" +
                                    " - - - - -\n" +
                                    " - - - - -\n" +
                                    " - - - - -\n";
            
            Assert.Equal(expected, result);

        }
    }
}