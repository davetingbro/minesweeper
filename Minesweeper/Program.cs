using System;

namespace Minesweeper
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var renderer = new ConsoleGameBoardRenderer();
                var gameUi = new ConsoleUi(renderer);
                var gameEngine = new GameEngine();
                var game = new Game(gameEngine, gameUi);
                
                game.Run();
            }
            catch (Exception e)
            {
                Console.WriteLine("We have a problem... we will fix it ASAP!");
                Console.WriteLine(e);
            }
        }
    }
}