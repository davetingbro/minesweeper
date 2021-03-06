using System;
using System.IO;
using Minesweeper.Enums;
using Xunit;

namespace Minesweeper.UnitTests
{
    [Collection("ConsoleUiTestCollection")]
    public class ConsoleGameBoardRendererTests
    {
        [Fact]
        public void ShouldRenderGameBoardAsExpected()
        {
            var gameBoard = CreateTestGameBoard();
            var consoleWriter = new StringWriter();
            Console.SetOut(consoleWriter);
            var renderer = new ConsoleGameBoardRenderer();

            renderer.Render(gameBoard);

            var result = consoleWriter.GetStringBuilder().ToString(); // store Console.WriteLine output in result
            const string expected = " - - - -\n" +
                                    " - 2 * -\n" +
                                    " - F - -\n" +
                                    " - - - -\n";
            
            Assert.Equal(expected, result);
        }

        private static GameBoard CreateTestGameBoard()
        {
            var gameBoard = new GameBoard(4, 4);
            gameBoard.BoardState[6].PlantMine();
            gameBoard.BoardState[5].AdjacentMineCount = 2;
            gameBoard.BoardState[5].CellState = CellState.Revealed;
            gameBoard.BoardState[9].CellState = CellState.Flagged;
            gameBoard.BoardState[6].CellState = CellState.Revealed;

            return gameBoard;
        }
    }
}