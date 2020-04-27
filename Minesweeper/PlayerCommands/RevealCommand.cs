using System.Collections.Generic;
using System.Linq;
using Minesweeper.Enums;
using Minesweeper.Exceptions;

namespace Minesweeper.PlayerCommands
{
    /// <summary>
    /// Implementation of the reveal logic when revealing a Cell on the GameBoard
    /// </summary>
    public class RevealCommand : PlayerCommand
    {
        public RevealCommand(Coordinate coordinate) : base(coordinate) {}

        public override void Execute(GameBoard gameBoard)
        {
            var cell = gameBoard.GetCell(Coordinate);
            ValidateMove(cell);
            gameBoard.BoardState = Reveal(gameBoard, cell);
        }

        private static void ValidateMove(Cell cell)
        {
            if (cell.CellState == CellState.Revealed)
                throw new InvalidMoveException("Invalid move: Cell already revealed.");
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