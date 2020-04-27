using Minesweeper.Enums;

namespace Minesweeper
{
    /// <summary>
    /// Data structure that represents the cell unit on the GameBoard
    /// </summary>
    public class Cell
    {
        public readonly int X;
        public readonly int Y;
        public CellState CellState { get; set; }
        public int AdjacentMineCount { get; set; }
        public bool IsMine { get; private set; }

        public Cell(Coordinate coordinate)
        {
            X = coordinate.X;
            Y = coordinate.Y;
            CellState = CellState.Unrevealed;
        }

        public void PlantMine()
        {
            IsMine = true;
        }
    }
}