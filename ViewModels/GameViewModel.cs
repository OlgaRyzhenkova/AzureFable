using AzureFable.Models;
using AzureFable.Services;
using System.Collections.ObjectModel;
using GameStateEnum = AzureFable.Models.GameState;

namespace AzureFable.ViewModels
{
    internal class GameViewModel : ViewModelBase
    {
        private Maze _maze;
        private readonly MazeGenerator _mazeGenerator;
        private readonly GameEngine _gameEngine;
        private readonly Action _onWin;
        private readonly Action _onGameOver;
        private readonly Action _onPause;
        private readonly Action _onHelp;
        private readonly GameSettings _settings;

        private GameStateEnum _gameState;
        public GameStateEnum GameState
        {
            get => _gameState;
            private set
            {
                if (_gameState == value)
                {
                    return;
                }

                _gameState = value;
                OnPropertyChanged();
                OnGameStateChanged(value);
            }
        }

        private int _heroHealth;
        public int HeroHealth
        {
            get => _heroHealth;
            private set
            {
                _heroHealth = value;
                OnPropertyChanged();
            }
        }

        private bool _hasKey;
        public bool HasKey
        {
            get => _hasKey;
            private set
            {
                _hasKey = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Cell> Cells { get; }
        public ObservableCollection<GameObject> GameObjects { get; }
        public ObservableCollection<bool> Hearts { get; }
        public RelayCommand PauseCommand { get; }
        public RelayCommand HelpCommand { get; }

        public GameViewModel(
            Action onWin,
            Action onGameOver,
            Action onPause,
            Action onHelp,
            GameSettings settings,
            IEnemyLogic enemyLogic,
            ICollisionService collisionService,
            IItemSpawnService itemSpawnService)
        {
            _onWin = onWin;
            _onGameOver = onGameOver;
            _onPause = onPause;
            _onHelp = onHelp;
            _settings = settings;
            _mazeGenerator = new MazeGenerator();
            GameObjects = new ObservableCollection<GameObject>();
            Cells = new ObservableCollection<Cell>();
            Hearts = new ObservableCollection<bool>();
            PauseCommand = new RelayCommand(Pause);
            HelpCommand = new RelayCommand(ShowHelp);

            _maze = _mazeGenerator.Generate();
            _gameEngine = new GameEngine(
                _maze,
                RefreshGame,
                _settings.EnemyMoveInterval,
                enemyLogic,
                collisionService,
                itemSpawnService);

            HeroHealth = _maze.Hero.Health;
            HasKey = _maze.Hero.HasKey;
            _gameState = _gameEngine.GameState;

            RefreshGame();
            _gameEngine.Start();
        }

        private void OnGameStateChanged(GameStateEnum state)
        {
            if (state == GameStateEnum.Win)
            {
                _gameEngine.Stop();
                _onWin();
            }
            else if (state == GameStateEnum.GameOver)
            {
                _gameEngine.Stop();
                _onGameOver();
            }
        }

        public void MoveHero(System.Windows.Input.Key key)
        {
            if (GameState != GameStateEnum.Playing)
            {
                return;
            }

            if (key == System.Windows.Input.Key.Escape)
            {
                Pause();
                return;
            }

            int dx = 0;
            int dy = 0;

            if (key == System.Windows.Input.Key.Up) dy = -1;
            else if (key == System.Windows.Input.Key.Down) dy = 1;
            else if (key == System.Windows.Input.Key.Left) dx = -1;
            else if (key == System.Windows.Input.Key.Right) dx = 1;
            else return;

            _gameEngine.MoveHero(dx, dy);
        }

        public void Pause()
        {
            if (GameState != GameStateEnum.Playing)
            {
                return;
            }

            _gameEngine.Pause();
            UpdateGameState();
            _onPause();
        }

        public void ShowHelp()
        {
            if (GameState != GameStateEnum.Playing)
            {
                return;
            }

            _gameEngine.Pause();
            UpdateGameState();
            _onHelp();
        }

        public void Resume()
        {
            if (GameState != GameStateEnum.Paused)
            {
                return;
            }

            _gameEngine.Resume();
            UpdateGameState();
        }

        private void RefreshGame()
        {
            UpdateGameState();
            UpdateGame();
        }

        private void UpdateGameState()
        {
            HeroHealth = _maze.Hero.Health;
            HasKey = _maze.Hero.HasKey;
            GameState = _gameEngine.GameState;

            Hearts.Clear();
            for (int i = 0; i < _maze.Hero.Health; i++)
            {
                Hearts.Add(true);
            }

        }

        private void UpdateGame()
        {
            Cells.Clear();
            GameObjects.Clear();

            for (int y = 0; y < _maze.Rows; y++)
            {
                for (int x = 0; x < _maze.Columns; x++)
                {
                    Cell cell = _maze.GetCell(x, y);
                    Cells.Add(cell);

                    if (cell.Item != null && cell.Item.IsActive)
                    {
                        GameObjects.Add(cell.Item);
                    }
                }
            }

            foreach (Enemy enemy in _maze.Enemies)
            {
                if (enemy.IsActive)
                {
                    GameObjects.Add(enemy);
                }
            }

            GameObjects.Add(_maze.Hero);
        }
    }
}
