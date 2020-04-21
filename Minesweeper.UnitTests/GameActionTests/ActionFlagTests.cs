using Minesweeper.Exceptions;
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
            var flagAction = new FlagAction(playCoordinate);

            var boardState = flagAction.GetNextBoardState(gameBoard);
            gameBoard.BoardState = boardState;

            var selectedCellState = gameBoard.GetCell(playCoordinate).CellState;
            Assert.Equal(CellState.Flagged, selectedCellState);
        }

        [Fact]
        public void ShouldThrowInvalidMoveExceptionIfCellIsAlreadyRevealed()
        {
            var gameBoard = new GameBoard(5, 5);
            var playCoordinate = new Coordinate(1, 1);
            gameBoard.GetCell(playCoordinate).CellState = CellState.Revealed;
            
            var flagAction = new FlagAction(playCoordinate);

            Assert.Throws<InvalidMoveException>(() => flagAction.GetNextBoardState(gameBoard));
        }
    }
}