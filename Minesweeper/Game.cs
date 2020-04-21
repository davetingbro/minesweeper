using System;
using Minesweeper.Interfaces;

namespace Minesweeper
{
    /// <summary>
    /// Execute the game logic on UI inputs
    /// </summary>
    public class Game
    {
        private IGameEngine _gameEngine;
        private IGameUi _gameUi;

        public Game(IGameEngine gameEngine, IGameUi gameUi)
        {
            _gameEngine = gameEngine;
            _gameUi = gameUi;
        }

        public void Play()
        {
            _gameUi.GetDimension();
            _gameUi.GetNumOfMines();
        }
    }
}