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
            var gameBoard = new GameBoard(8, 8);
            var gameEngine = new GameEngine(gameBoard);
            var playCoordinate = new Coordinate(1, 1);
            var action = new Action(actionType, playCoordinate);
            
            gameEngine.PlayUserAction(action);

            var revealedCell = gameEngine.GameBoard.GetCell(playCoordinate);
            var result = revealedCell.CellState;
            
            Assert.Equal(expected, result);
        }
    }
}