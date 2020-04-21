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
            InitializeGame();
        }

        private void InitializeGame()
        {
            _gameEngine.GameBoard = _gameUi.GetDimension();
            _gameEngine.NumOfMines = _gameUi.GetNumOfMines();
            _gameEngine.Initialize();
        }
    }
}