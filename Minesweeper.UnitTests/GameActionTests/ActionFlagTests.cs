using Minesweeper.GameActions;
using Xunit;

namespace Minesweeper.UnitTests.GameActionTests
{
    public class ActionFlagTests
    {
        [Fact]
        public void ShouldSetSelectedCellStateToFlagged()
        {
            var gameBoard = new GameBoard(5, 5);
            var playCoordinate = new Coordinate(1, 1);
            var actionFlag = new ActionFlag(playCoordinate);

            var boardState = actionFlag.GetNextBoardState(gameBoard);
            gameBoard.BoardState = boardState;

            var selectedCellState = gameBoard.GetCell(playCoordinate).CellState;
            Assert.Equal(CellState.Flagged, selectedCellState);
        }
    }
}