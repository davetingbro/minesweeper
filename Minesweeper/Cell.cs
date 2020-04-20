using System;
using Minesweeper.Exceptions;

namespace Minesweeper
{
    /// <summary>
    /// Object that stores all information of a cell on the game board
    /// </summary>
    public class Cell
    {
        public readonly int X;
        public readonly int Y;
        public CellState CellState { get; private set; }
        public int AdjacentMineCount { get; set; }
        public bool IsMine { get; private set; }

        public Cell(Coordinate coordinate)
        {
            X = coordinate.X;
            Y = coordinate.Y;
            CellState = CellState.Unrevealed;
        }

        public Cell(CellState cellState, int adjacentMineCount, bool isMine)
        {
            CellState = cellState;
            AdjacentMineCount = adjacentMineCount;
            IsMine = isMine;
        }

        public void PlantMine()
        {
            IsMine = true;
        }

        public void Reveal()
        {
            CellState = CellState.Revealed;
        }

        public void Flag()
        {
            if (CellState == CellState.Revealed)
                throw new InvalidMoveException("Invalid move: Cannot flag cell that is already revealed.");
            CellState = CellState.Flagged;
        }

        public override string ToString()
        {
            var output = CellState switch
            {
                CellState.Revealed => GetCellContent(),
                CellState.Unrevealed => "-",
                CellState.Flagged => "F",
                _ => throw new ArgumentOutOfRangeException()
            };

            return output;
        }

        private string GetCellContent()
        {
            return IsMine ? "*" : AdjacentMineCount.ToString();
        }
    }
}