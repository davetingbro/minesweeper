using System.Linq;
using Minesweeper.GameActions;
using Moq;
using Xunit;

namespace Minesweeper.UnitTests
{
    public class GameEngineTests
    {
        [Fact]
        public void NumOfMinesPlantedShouldEqualNumOfMines_WhenInitialize()
        {
            var gameBoard = new GameBoard(5, 5);
            const int numOfMines = 5;
            var gameEngine = new GameEngine(gameBoard, numOfMines);

            gameEngine.Initialize();

            var numOfMinePlanted = gameEngine.GameBoard.BoardState.Count(c => c.IsMine);
            Assert.Equal(5, numOfMinePlanted);
        }

        [Fact]
        public void ShouldCallGameBoardSetAllCellAdjacentMineCountMethod_WhenInitialize()
        {
            var mockGameBoard = new Mock<GameBoard>(5, 5);
            const int numOfMines = 5;
            var gameEngine = new GameEngine(mockGameBoard.Object, numOfMines);

            gameEngine.Initialize();
            
            mockGameBoard.Verify(gb => gb.SetAllCellAdjacentMineCount(), 
                Times.Once);
        }

        [Fact]
        public void ShouldCallGameActionGetNextBoardStateMethod_WhenPlayUserAction()
        {
            var stubGameBoard = new Mock<GameBoard>(5, 5);
            var mockGameAction = MockGameAction(stubGameBoard.Object);
            var gameEngine = new GameEngine(stubGameBoard.Object, 0);

            gameEngine.PlayUserAction(mockGameAction.Object);
            
            mockGameAction.Verify(action => action.GetNextBoardState(stubGameBoard.Object), Times.Once);
        }

        private static Mock<GameAction> MockGameAction(GameBoard gameBoard)
        {
            var stubCoordinate = new Mock<Coordinate>(1, 1);
            var mockGameAction = new Mock<GameAction>(stubCoordinate.Object);
            mockGameAction
                .Setup(action => action.GetNextBoardState(gameBoard))
                .Returns(new GameBoard(5, 5).BoardState);
            return mockGameAction;
        }

        [Fact]
        public void ShouldSetIsGameFinishedToTrue_WhenRevealAMine()
        {
            var mineCoordinate = new Coordinate(2, 3);
            var gameBoard = SetupGameBoardWithMines(mineCoordinate);
            
            var gameEngine = new GameEngine(gameBoard, 1);
            var action = new RevealAction(mineCoordinate);
            
            gameEngine.PlayUserAction(action);
            
            Assert.True(gameEngine.IsGameFinished);
        }

        [Fact]
        public void ShouldSetIsGameFinishedAndIsPlayerWinToTrue_WhenAllMinesAreFlagged()
        {
            var mineCoordinate1 = new Coordinate(2, 3);
            var mineCoordinate2 = new Coordinate(5, 7);
            var gameBoard = SetupGameBoardWithMines(mineCoordinate1, mineCoordinate2);
            
            var gameEngine = new GameEngine(gameBoard, 2);
            var actions = new []{new FlagAction(mineCoordinate1), new FlagAction(mineCoordinate2) };

            foreach (var action in actions)
            {
                gameEngine.PlayUserAction(action);
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