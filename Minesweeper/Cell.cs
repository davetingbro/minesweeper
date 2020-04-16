namespace Minesweeper
{
    /// <summary>
    /// Object that stores all information of a cell on the game board
    /// </summary>
    public class Cell
    {
        public Coordinate Coordinate;
        public int NumOfNearbyMine;
        public CellState CellState { get; set; } = CellState.Unrevealed;
        public bool IsMine { get; private set; }

        public Cell(Coordinate coordinate)
        {
            Coordinate = coordinate;
        }

        public void PlantMine()
        {
            IsMine = true;
        }
    }
}