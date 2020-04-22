using Minesweeper.Enums;
using Minesweeper.GameActions;
using Xunit;

namespace Minesweeper.UnitTests.GameActionTests
{
    public class UnflagActionTests
    {
        [Fact]
        public void ShouldSetSelectedCellStateToUnflagged()
        {
            var gameBoard = new GameBoard(5, 5);
            var flagCoordinate = new Coordinate(1, 1);
            gameBoard.GetCell(flagCoordinate).CellState = CellState.Flagged;
            
            var unflagAction = new UnflagAction(flagCoordinate);
            gameBoard.BoardState = unflagAction.GetNextBoardState(gameBoard);

            var result = gameBoard.GetCell(flagCoordinate).CellState;
            Assert.Equal(CellState.Unrevealed, result);
        }
    }
}