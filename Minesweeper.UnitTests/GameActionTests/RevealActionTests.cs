using Minesweeper.GameActions;
using Xunit;

namespace Minesweeper.UnitTests.GameActionTests
{
    public class RevealActionTests
    {
        [Fact]
        public void ShouldSetSelectedCellStateToRevealed()
        {
            var gameBoard = new GameBoard(5, 5);
            var playCoordinate = new Coordinate(1, 1);
            var revealAction = new RevealAction(playCoordinate);

            var nextBoardState = revealAction.GetNextBoardState(gameBoard);
            gameBoard.BoardState = nextBoardState;

            var result = gameBoard.GetCell(playCoordinate).CellState;
            Assert.Equal(CellState.Revealed, result);
        }
    }
}