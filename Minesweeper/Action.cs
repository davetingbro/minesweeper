namespace Minesweeper
{
    public class Action
    {
        public ActionType ActionType;
        public Coordinate Coordinate;

        public Action(ActionType actionType, Coordinate coordinate)
        {
            ActionType = actionType;
            Coordinate = coordinate;
        }
    }
}