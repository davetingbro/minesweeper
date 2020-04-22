using System.Collections.Generic;
using System.Linq;
using Minesweeper.Enums;
using Minesweeper.GameActions;
using Newtonsoft.Json;
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

        [Fact]
        public void ShouldMatchExpectedBoardState()
        {
            var mineCoordinate = new Coordinate(2, 3);
            var gameBoard = SetupGameBoardWithMine(mineCoordinate);

            var playCoordinate = new Coordinate(1, 1);
            var revealAction = new RevealAction(playCoordinate);

            var boardState = revealAction.GetNextBoardState(gameBoard).Select(c => c.CellState).ToList();
            var expectedBoardState = new List<CellState>
            {
                CellState.Revealed, CellState.Revealed, CellState.Revealed, CellState.Revealed,
                CellState.Revealed, CellState.Revealed, CellState.Revealed, CellState.Revealed,
                CellState.Unrevealed, CellState.Unrevealed, CellState.Revealed, CellState.Revealed,
                CellState.Unrevealed, CellState.Unrevealed, CellState.Revealed, CellState.Revealed,
            };

            Assert.True(boardState.SequenceEqual(expectedBoardState));
        }

        private static GameBoard SetupGameBoardWithMine(Coordinate mineCoordinate)
        {
            var gameBoard = new GameBoard(4, 4);
            gameBoard.GetCell(mineCoordinate).PlantMine();
            gameBoard.SetAllCellAdjacentMineCount();
            return gameBoard;
        }
    }
}