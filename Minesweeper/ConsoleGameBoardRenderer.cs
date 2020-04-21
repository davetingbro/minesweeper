using System;
using System.Linq;
using Minesweeper.Interfaces;

namespace Minesweeper
{
    /// <summary>
    /// Implementation of GameBoard rendering logic
    /// </summary>
    public class ConsoleGameBoardRenderer : IGameBoardRenderer
    {
        public void Render(GameBoard gameBoard)
        {
            var gameBoardString = ParseGameBoardToString(gameBoard);
            Console.Write(gameBoardString);
        }

        private string ParseGameBoardToString(GameBoard gameBoard)
        {
            var cellStrings = gameBoard.Cells
                .Select((c, i) =>
                {
                    var isEndOfLine = (i + 1) % gameBoard.Width == 0;
                    var cellString = ParseCellToString(c);
                    return isEndOfLine ? $" {cellString}\n" : $" {cellString}";
                });

            return string.Join("", cellStrings);
        }

        private string ParseCellToString(Cell cell)
        {
            var output = cell.CellState switch
            {
                CellState.Revealed => GetCellContent(cell),
                CellState.Unrevealed => "-",
                CellState.Flagged => "F",
                _ => throw new ArgumentOutOfRangeException()
            };

            return output;
        }
        
        private static string GetCellContent(Cell cell)
        {
            return cell.IsMine ? "*" : cell.AdjacentMineCount.ToString();
        }
    }
}