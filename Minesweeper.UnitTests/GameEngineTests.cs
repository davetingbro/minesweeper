using System.Collections.Generic;
using System.Linq;
using Minesweeper.Enums;
using Minesweeper.Exceptions;
using Minesweeper.PlayerCommands;
using Moq;
using Xunit;

namespace Minesweeper.UnitTests
{
    public class GameEngineTests
    {
        [Fact]
        public void NumOfMinesPlantedShouldEqualNumOfMinesSet_WhenInitialized()
        {
            var gameEngine = new GameEngine {GameBoard = new GameBoard(5, 5), NumOfMines = 5};

            gameEngine.Initialize();

            var numOfMinePlanted = gameEngine.GameBoard.BoardState.Count(c => c.IsMine);
            Assert.Equal(5, numOfMinePlanted);
        }

        [Fact]
        public void ShouldThrowInvalidInputException_WhenGivenNumOfMineValueIsGreaterThanBoardSize()
        {
            var gameEngine = new GameEngine {GameBoard = new GameBoard(5, 5)};

            Assert.Throws<InvalidInputException>(() => gameEngine.NumOfMines = 26);
        }

        [Fact]
        public void ShouldSetCellAdjacentMineCount_WhenInitialized()
        {
            var gameBoard = new GameBoard(5, 5);
            gameBoard.BoardState[6].PlantMine();
            gameBoard.BoardState[16].PlantMine();
            gameBoard.BoardState[18].PlantMine();
            // Mine locations:
            //     - - - - -
            //     - * - - -
            //     - - - - -
            //     - * - * -
            //     - - - - -
            
            var gameEngine = new GameEngine
            {
                GameBoard = gameBoard,
            };

            gameEngine.Initialize();

            var result = gameEngine.GameBoard.BoardState.Select(c => c.AdjacentMineCount).ToList();
            var expected = new List<int>
            {
                1, 1, 1, 0, 0,
                1, 0, 1, 0, 0,
                2, 2, 3, 1, 1,
                1, 0, 2, 0, 1,
                1, 1, 2, 1, 1,
            };

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ShouldCallPlayerCommandExecute_WhenExecutePlayerCommand()
        {
            var stubGameBoard = new Mock<GameBoard>(5, 5);
            var mockPlayerCommand = new Mock<PlayerCommand>(new Coordinate(1, 1));
            var gameEngine = new GameEngine {GameBoard = stubGameBoard.Object};

            gameEngine.ExecutePlayerCommand(mockPlayerCommand.Object);
            
            mockPlayerCommand.Verify(action => action.Execute(stubGameBoard.Object), Times.Once);
        }

        [Fact]
        public void ShouldThrowInvalidMoveException_WhenGivenCoordinateOutOfBoardRange()
        {
            var stubGameBoard = new Mock<GameBoard>(5, 5);
            var mockPlayerCommand = new Mock<PlayerCommand>(new Coordinate(6, 6));
            var gameEngine = new GameEngine {GameBoard = stubGameBoard.Object};

            Assert.Throws<InvalidMoveException>(() => gameEngine.ExecutePlayerCommand(mockPlayerCommand.Object));
        }

        [Fact]
        public void ShouldSetIsGameFinishedToTrue_WhenRevealAMine()
        {
            var mineCoordinate = new Coordinate(2, 3);
            var gameBoard = SetupGameBoardWithMines(mineCoordinate);
            var gameEngine = new GameEngine { GameBoard = gameBoard, NumOfMines = 1 };
            
            var mockRevealCommand = new Mock<PlayerCommand>(mineCoordinate);
            mockRevealCommand
                .Setup(c => c.Execute(gameBoard))
                .Callback((() => gameBoard.GetCell(mineCoordinate).CellState = CellState.Revealed));
            
            gameEngine.ExecutePlayerCommand(mockRevealCommand.Object);
            
            Assert.True(gameEngine.IsGameFinished);
            Assert.False(gameEngine.IsPlayerWin);
        }

        [Fact]
        public void ShouldSetIsGameFinishedAndIsPlayerWinToTrue_WhenAllMinesAreFlagged()
        {
            var mineCoordinates = new[] { new Coordinate(2, 3), new Coordinate(5, 7)};
            var gameBoard = SetupGameBoardWithMines(mineCoordinates); // plant two mines on gameBoard
            var gameEngine = new GameEngine { GameBoard = gameBoard, NumOfMines = 2 };

            // two flag commands that flag both mines
            var mockFlagCommands = SetupMockFlagCommands(mineCoordinates, gameBoard);

            foreach (var command in mockFlagCommands)
            {
                gameEngine.ExecutePlayerCommand(command);
            }

            Assert.True(gameEngine.IsGameFinished);
            Assert.True(gameEngine.IsPlayerWin);
        }

        private static GameBoard SetupGameBoardWithMines(params Coordinate[] coordinates)
        {
            var gameBoard = new GameBoard(8, 8);
            foreach (var coordinate in coordinates)
            {
                gameBoard.GetCell(coordinate).PlantMine();
            }

            return gameBoard;
        }

        private static IEnumerable<PlayerCommand> SetupMockFlagCommands(Coordinate[] mineCoordinates, GameBoard gameBoard)
        {
            return mineCoordinates
                .Select(coordinate =>
                {
                    var mockCommand = new Mock<PlayerCommand>(coordinate);
                    mockCommand
                        .Setup(command => command.Execute(gameBoard))
                        .Callback((() => gameBoard.GetCell(coordinate).CellState = CellState.Flagged));
                    return mockCommand.Object;
                });
        }
    }
}