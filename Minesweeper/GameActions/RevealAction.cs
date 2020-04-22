using System.Collections.Generic;
using System.Linq;
using Minesweeper.Enums;

namespace Minesweeper.GameActions
{
    public class RevealAction : GameAction
    {
        public RevealAction(Coordinate coordinate) : base(coordinate) {}

        public override List<Cell> GetNextBoardState(GameBoard gameBoard)
        {
            var cell = gameBoard.GetCell(Coordinate);
            var newBoardState = Reveal(cell, gameBoard);
            return newBoardState;
        }

        private static List<Cell> Reveal(Cell cell, GameBoard gameBoard)
        {
            cell.CellState = CellState.Revealed;
            var boardState = gameBoard.BoardState;
            if (cell.AdjacentMineCount > 0)
                return boardState;

            var nonMineNeighbours = gameBoard.GetCellNeighbours(cell)
                .Where(c => !c.IsMine && c.CellState != CellState.Revealed);
            
            foreach (var neighbour in nonMineNeighbours)
            {
                boardState = Reveal(neighbour, gameBoard);
            }

            return boardState;
        }
    }
}