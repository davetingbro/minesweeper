using System.Linq;
using Minesweeper.PlayerCommands;
using Moq;
using Xunit;

namespace Minesweeper.UnitTests
{
    public class GameEngineTests
    {
        [Fact]
        public void NumOfMinesPlantedShouldEqualNumOfMines_WhenInitialize()
        {
            var gameEngine = new GameEngine
            {
                GameBoard = new GameBoard(5, 5),
                NumOfMines = 5
            };

            gameEngine.Initialize();

            var numOfMinePlanted = gameEngine.GameBoard.BoardState.Count(c => c.IsMine);
            Assert.Equal(5, numOfMinePlanted);
        }

        [Fact]
        public void ShouldCallGameBoardSetAllCellAdjacentMineCountMethod_WhenInitialize()
        {
            var mockGameBoard = new Mock<GameBoard>(5, 5);
            var gameEngine = new GameEngine
            {
                GameBoard = mockGameBoard.Object,
                NumOfMines = 5
            };

            gameEngine.Initialize();
            
            mockGameBoard.Verify(gb => gb.SetAllCellAdjacentMineCount(), 
                Times.Once);
        }

        [Fact]
        public void ShouldCallPlayerCommandExecute_WhenPlayUserAction()
        {
            var stubGameBoard = new Mock<GameBoard>(5, 5);
            var mockPlayerCommand = new Mock<PlayerCommand>(new Coordinate(1, 1));
            var gameEngine = new GameEngine
            {
                GameBoard = stubGameBoard.Object,
                NumOfMines = 0
            };

            gameEngine.PlayPlayerCommand(mockPlayerCommand.Object);
            
            mockPlayerCommand.Verify(action => action.Execute(stubGameBoard.Object), Times.Once);
        }

        [Fact]
        public void ShouldSetIsGameFinishedToTrue_WhenRevealAMine()
        {
            var mineCoordinate = new Coordinate(2, 3);
            var gameEngine = new GameEngine
            {
                GameBoard = SetupGameBoardWithMines(mineCoordinate),
                NumOfMines = 1
            };
            var command = new RevealCommand(mineCoordinate);
            
            gameEngine.PlayPlayerCommand(command);
            
            Assert.True(gameEngine.IsGameFinished);
            Assert.False(gameEngine.IsPlayerWin);
        }

        [Fact]
        public void ShouldSetIsGameFinishedAndIsPlayerWinToTrue_WhenAllMinesAreFlagged()
        {
            var mineCoordinate1 = new Coordinate(2, 3);
            var mineCoordinate2 = new Coordinate(5, 7);
            var gameEngine = new GameEngine
            {
                GameBoard = SetupGameBoardWithMines(mineCoordinate1, mineCoordinate2),
                NumOfMines = 2
            };
            var flagCommands = new []{new FlagCommand(mineCoordinate1), new FlagCommand(mineCoordinate2) };

            foreach (var action in flagCommands)
            {
                gameEngine.PlayPlayerCommand(action);
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
    }
}