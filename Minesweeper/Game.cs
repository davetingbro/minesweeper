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
            PlayGame();
        }

        private void InitializeGame()
        {
            _gameEngine.GameBoard = _gameUi.GetDimension();
            _gameEngine.NumOfMines = _gameUi.GetNumOfMines();
            _gameEngine.Initialize();
        }

        private void PlayGame()
        {
            while (!_gameEngine.IsGameFinished)
            {
                var action = _gameUi.GetUserAction();
                _gameEngine.PlayUserAction(action);
            }
            
            _gameUi.PrintResult(_gameEngine.IsPlayerWin);
        }
    }
}