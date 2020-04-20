using System.Linq;
using Moq;
using Xunit;

namespace Minesweeper.UnitTests
{
    public class GameEngineTests
    {
        [Fact]
        public void NumOfPlantMineCallsShouldEqualNumOfMines_WhenInitialize()
        {
            var mockGameBoard = new Mock<GameBoard>(5, 5);
            const int numOfMines = 5;
            var gameEngine = new GameEngine(mockGameBoard.Object, numOfMines);

            gameEngine.Initialize();
            
            mockGameBoard.Verify(gb => gb.PlantMine(It.IsAny<int>()), 
                Times.Exactly(numOfMines));
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

        [Theory]
        [InlineData(ActionType.Reveal, CellState.Revealed)]
        [InlineData(ActionType.Flag, CellState.Flagged)]
        public void ShouldSetCellStateCorrectlyByActionType(ActionType actionType, CellState expected)
        {
            var gameEngine = new GameEngine(new GameBoard(8, 8), 0);
            var playCoordinate = new Coordinate(1, 1);
            var action = new Action(actionType, playCoordinate);
            
            gameEngine.PlayUserAction(action);

            var affectedCell = gameEngine.GameBoard.GetCell(playCoordinate);
            var result = affectedCell.CellState;
            
            Assert.Equal(expected, result);
        }

        [Fact]
        public void ShouldSetIsGameFinishedToTrue_WhenRevealAMine()
        {
            var mineCoordinate = new Coordinate(2, 3);
            var gameBoard = SetupGameBoardWithMines(mineCoordinate);
            
            var gameEngine = new GameEngine(gameBoard, 1);
            var action = new Action(ActionType.Reveal, mineCoordinate);
            
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
            var actions = SetupActions(ActionType.Flag, mineCoordinate1, mineCoordinate2);

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

        private static Action[] SetupActions(ActionType actionType, params Coordinate[] coordinates)
        {
            return coordinates.Select(c => new Action(actionType, c)).ToArray();
        }
    }
}