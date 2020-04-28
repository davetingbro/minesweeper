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
            while (_gameEngine.GameBoard == null || _gameEngine.NumOfMines == 0)
            {
                try
                {
                    _gameEngine.GameBoard ??= _gameUi.GetDimension();
                    var boardSize = _gameEngine.GameBoard.BoardState.Count;
                    var numOfMines = _gameUi.GetNumOfMines();
                    _gameEngine.NumOfMines = numOfMines < boardSize ? numOfMines 
                        : throw new InvalidInputException("Invalid Input: cannot have more mines than the board size.");
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
                    var action = _gameUi.GetPlayerCommand();
                    _gameEngine.ExecutePlayerCommand(action);
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