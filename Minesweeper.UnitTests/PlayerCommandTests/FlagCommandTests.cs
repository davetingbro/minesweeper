using Minesweeper.Enums;
using Minesweeper.Exceptions;
using Minesweeper.PlayerCommands;
using Xunit;

namespace Minesweeper.UnitTests.PlayerCommandTests
{
    public class FlagCommandTests
    {
        private readonly GameBoard _gameBoard;
        public FlagCommandTests()
        {
            _gameBoard = new GameBoard(5, 5);
        }
        
        [Fact]
        public void ShouldSetSelectedCellStateToFlagged()
        {
            var flagCoordinate = new Coordinate(1, 1);
            var flagCommand = new FlagCommand(flagCoordinate);
            
            flagCommand.Execute(_gameBoard);

            var result = _gameBoard.GetCell(flagCoordinate).CellState;
            Assert.Equal(CellState.Flagged, result);
        }

        [Fact]
        public void ShouldSetCellStateToUnrevealedIfAlreadyFlagged()
        {
            var flagCoordinate = new Coordinate(1, 1);
            _gameBoard.GetCell(flagCoordinate).CellState = CellState.Flagged;
            var flagCommand = new FlagCommand(flagCoordinate);

            flagCommand.Execute(_gameBoard);

            var result = _gameBoard.GetCell(flagCoordinate).CellState;
            Assert.Equal(CellState.Unrevealed, result);
        }

        [Fact]
        public void ShouldThrowInvalidMoveExceptionIfCellIsAlreadyRevealed()
        {
            var revealedCoordinate = new Coordinate(1, 1);
            _gameBoard.GetCell(revealedCoordinate).CellState = CellState.Revealed;
            
            var flagCommand = new FlagCommand(revealedCoordinate);

            Assert.Throws<InvalidMoveException>(() => flagCommand.Execute(_gameBoard));
        }
    }
}