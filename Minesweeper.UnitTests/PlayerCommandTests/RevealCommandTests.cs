using System.Collections.Generic;
using System.Linq;
using Minesweeper.Enums;
using Minesweeper.Exceptions;
using Minesweeper.PlayerCommands;
using Newtonsoft.Json;
using Xunit;

namespace Minesweeper.UnitTests.PlayerCommandTests
{
    public class RevealCommandTests
    {
        [Fact]
        public void ShouldSetSelectedCellStateToRevealed_WhenExecuted()
        {
            var gameBoard = new GameBoard(5, 5);
            var playCoordinate = new Coordinate(1, 1);
            var revealCommand = new RevealCommand(playCoordinate);
            
            revealCommand.Execute(gameBoard);

            var result = gameBoard.GetCell(playCoordinate).CellState;
            Assert.Equal(CellState.Revealed, result);
        }

        [Fact]
        public void ShouldThrowInvalidMoveException_WhenExecuteOnRevealedCell()
        {
            var gameBoard = new GameBoard(5, 5);
            var revealCoordinate = new Coordinate(1, 1);
            gameBoard.GetCell(revealCoordinate).CellState = CellState.Revealed;
            
            var revealCommand = new RevealCommand(revealCoordinate);

            Assert.Throws<InvalidMoveException>(() => revealCommand.Execute(gameBoard));
        }

        [Fact]
        public void ShouldMatchExpectedBoardState()
        {
            var mineCountMap = new List<int>
            {
                0, 0, 0, 0,
                1, 1, 1, 0,
                1, 0, 1, 0,
                1, 1 ,1, 0
            };
            var gameBoard = SetupGameBoardWithMineCounts(mineCountMap);

            var playCoordinate = new Coordinate(1, 1);
            var revealCommand = new RevealCommand(playCoordinate);

            revealCommand.Execute(gameBoard);

            var result = gameBoard.BoardState.Select(c => c.CellState);
            var expectedBoardState = new List<CellState>
            {
                CellState.Revealed, CellState.Revealed, CellState.Revealed, CellState.Revealed,
                CellState.Revealed, CellState.Revealed, CellState.Revealed, CellState.Revealed,
                CellState.Unrevealed, CellState.Unrevealed, CellState.Revealed, CellState.Revealed,
                CellState.Unrevealed, CellState.Unrevealed, CellState.Revealed, CellState.Revealed,
            };

            Assert.Equal(expectedBoardState, result);
        }

        private static GameBoard SetupGameBoardWithMineCounts(List<int> mineCountMap)
        {
            var gameBoard = new GameBoard(4, 4);
            for (var i = 0; i < mineCountMap.Count; i++)
            {
                gameBoard.BoardState[i].AdjacentMineCount = mineCountMap[i];
            }
            return gameBoard;
        }
    }
}