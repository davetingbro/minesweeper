using System;
using Minesweeper.Exceptions;
using Minesweeper.Interfaces;

namespace Minesweeper
{
    /// <summary>
    /// Represent the Minesweeper game - calls on the GameEngine and GameUi public method to run the game
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
            GameBoard gameBoard = null;
            var numOfMines = 0;
            while (gameBoard == null || numOfMines == 0)
            {
                try
                {
                    gameBoard ??= _gameUi.GetDimension();
                    numOfMines = _gameUi.GetNumOfMines();
                }
                catch (GameException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            _gameEngine.GameBoard = gameBoard;
            _gameEngine.NumOfMines = numOfMines;
        }

        private void PlayGame()
        {
            while (!_gameEngine.IsGameFinished)
            {
                try
                {
                    var command = _gameUi.GetPlayerCommand();
                    _gameEngine.ExecutePlayerCommand(command);
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