using System.Collections.Generic;
using System.Linq;
using Minesweeper.Enums;

namespace Minesweeper.PlayerCommands
{
    public class RevealCommand : PlayerCommand
    {
        public RevealCommand(Coordinate coordinate) : base(coordinate) {}

        public override void Execute(GameBoard gameBoard)
        {
            var cell = gameBoard.GetCell(Coordinate);
            var newBoardState = Reveal(gameBoard, cell);
            gameBoard.BoardState = newBoardState;
        }

        private static List<Cell> Reveal(GameBoard gameBoard, Cell cell)
        {
            cell.CellState = CellState.Revealed;
            var boardState = gameBoard.BoardState;
            if (cell.AdjacentMineCount > 0)
                return boardState;

            var nonMineNeighbours = gameBoard.GetCellNeighbours(cell)
                .Where(c => !c.IsMine && c.CellState != CellState.Revealed);
            
            foreach (var neighbour in nonMineNeighbours)
            {
                boardState = Reveal(gameBoard, neighbour);
            }

            return boardState;
        }
    }
}