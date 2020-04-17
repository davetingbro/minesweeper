namespace Minesweeper
{
    /// <summary>
    /// Object that stores all information of a cell on the game board
    /// </summary>
    public class Cell
    {
        public readonly int X;
        public readonly int Y;
        public CellState CellState { get; set; } = CellState.Unrevealed;
        public int AdjacentMineCount { get; set; }
        public bool IsMine { get; private set; }

        public Cell(Coordinate coordinate)
        {
            X = coordinate.X;
            Y = coordinate.Y;
        }

        public void PlantMine()
        {
            IsMine = true;
        }
    }
}