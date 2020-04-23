using System;
using Minesweeper.Exceptions;
using Minesweeper.GameActions;
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
            while (_gameEngine.GameBoard == null || _gameEngine.NumOfMines == 0)
            {
                try
                {
                    _gameEngine.GameBoard ??= _gameUi.GetDimension();
                    _gameEngine.NumOfMines = _gameUi.GetNumOfMines();
                }
                catch (InvalidInputException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            _gameEngine.Initialize();
        }

        private void PlayGame()
        {
            while (!_gameEngine.IsGameFinished)
            {
                var action = GetUserAction();
                if (action == null) continue;
                
                _gameEngine.PlayUserAction(action);
                _gameUi.DisplayGameBoard(_gameEngine.GameBoard);
            }
            
            _gameUi.PrintResult(_gameEngine.IsPlayerWin);
        }

        private GameAction GetUserAction()
        {
            GameAction action = null;
            try
            {
                action = _gameUi.GetUserAction();
            }
            catch (GameException e)
            {
                Console.WriteLine(e.Message);
            }

            return action;
        }
    }
}