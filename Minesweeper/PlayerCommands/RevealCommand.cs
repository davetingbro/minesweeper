using System.Collections.Generic;
using System.Linq;
using Minesweeper.Enums;
using Minesweeper.Exceptions;

namespace Minesweeper.PlayerCommands
{
    public class RevealCommand : PlayerCommand
    {
        public RevealCommand(Coordinate coordinate) : base(coordinate) {}

        public override void Execute(GameBoard gameBoard)
        {
            var cell = gameBoard.GetCell(Coordinate);
            Validate(cell);
            gameBoard.BoardState = Reveal(gameBoard, cell);
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

        private static void Validate(Cell cell)
        {
            if (cell.CellState == CellState.Revealed)
                throw new InvalidMoveException("Invalid move: Cell already revealed.");
        }
    }
}