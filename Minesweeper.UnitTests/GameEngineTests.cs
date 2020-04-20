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
            var gameEngine = new GameEngine(mockGameBoard.Object);
            const int numOfMines = 5;
            
            gameEngine.Initialize(numOfMines);
            
            mockGameBoard.Verify(gb => gb.PlantMine(It.IsAny<int>()), 
                Times.Exactly(numOfMines));
        }

        [Fact]
        public void ShouldCallGameBoardSetAllCellAdjacentMineCountMethod_WhenInitialize()
        {
            var mockGameBoard = new Mock<GameBoard>(5, 5);
            var gameEngine = new GameEngine(mockGameBoard.Object);
            const int numOfMines = 5;
            
            gameEngine.Initialize(numOfMines);
            
            mockGameBoard.Verify(gb => gb.SetAllCellAdjacentMineCount(), 
                Times.Once);
        }

        [Theory]
        [InlineData(ActionType.Reveal, CellState.Revealed)]
        [InlineData(ActionType.Flag, CellState.Flagged)]
        public void ShouldSetCellStateCorrectlyByActionType(ActionType actionType, CellState expected)
        {
            var gameEngine = new GameEngine(new GameBoard(8, 8));
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
            var gameBoard = new GameBoard(8, 8);
            gameBoard.GetCell(mineCoordinate).PlantMine();
            var gameEngine = new GameEngine(gameBoard);
            
            var action = new Action(ActionType.Reveal, mineCoordinate);
            gameEngine.PlayUserAction(action);
            
            Assert.True(gameEngine.IsGameFinished);
        }
    }
}