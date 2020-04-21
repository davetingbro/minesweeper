using System;
using System.IO;
using Xunit;

namespace Minesweeper.UnitTests
{
    public class ConsoleGameBoardRendererTests
    {
        [Fact]
        public void ShouldRenderGameBoardAsExpected()
        {
            var gameBoard = CreateTestGameBoard();
            
            var renderer = new ConsoleGameBoardRenderer();
            var consoleWriter = new StringWriter();
            Console.SetOut(consoleWriter);
            
            renderer.Render(gameBoard);

            var result = consoleWriter.GetStringBuilder().ToString();
            const string expected = " - - - -\n" +
                                    " - 2 * -\n" +
                                    " - F - -\n" +
                                    " - - - -\n";
            
            Assert.Equal(expected, result);
        }

        private static GameBoard CreateTestGameBoard()
        {
            var gameBoard = new GameBoard(4, 4);
            gameBoard.PlantMine(6);
            gameBoard.PlantMine(9);
            gameBoard.SetAllCellAdjacentMineCount();
            gameBoard.BoardState[5].Reveal();
            gameBoard.BoardState[9].Flag();
            gameBoard.BoardState[6].Reveal();

            return gameBoard;
        }
    }
}