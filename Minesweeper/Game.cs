using System;
using Minesweeper.Exceptions;
using Minesweeper.Interfaces;

namespace Minesweeper
{
    /// <summary>
    /// Execute the game logic on UI inputs
    /// </summary>
    public class Game
    {
        private readonly IGameEngine _gameEngine;
        private readonly IGameUi _gameUi;

        public Game(IGameEngine gameEngine, IGameUi gameUi)
        {
            _gameEngine = gameEngine;
            _gameUi = gameUi;
        }

        public void Run()
        {
            InitializeGame();
            _gameUi.DisplayGameBoard(_gameEngine.GameBoard);
            PlayGame();
        }

        private void InitializeGame()
        {
            SetGameState();
            _gameEngine.Initialize();
        }

        private void SetGameState()
        {
            while (_gameEngine.GameBoard == null || _gameEngine.NumOfMines == 0)
            {
                try
                {
                    _gameEngine.GameBoard ??= _gameUi.GetDimension();
                    _gameEngine.NumOfMines = _gameUi.GetNumOfMines();
                }
                catch (GameException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        private void PlayGame()
        {
            while (!_gameEngine.IsGameFinished)
            {
                try
                {
                    var action = _gameUi.GetUserAction();
                    _gameEngine.PlayPlayerCommand(action);
                }
                catch (GameException e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }
                
                _gameUi.DisplayGameBoard(_gameEngine.GameBoard);
            }
            
            _gameUi.PrintResult(_gameEngine.IsPlayerWin);
        }
    }
}