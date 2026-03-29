using AzureFable.Models;
using AzureFable.Services;
using System.Collections.ObjectModel;
using System.Windows;

namespace AzureFable.ViewModels
{
    internal class GameViewModel : ViewModelBase
    {
        private Maze _maze;
        private readonly MazeGenerator _mazeGenerator;
        private readonly CollisionService _collisionService;
        private readonly GameEngine _gameEngine;
        private readonly Action _onWin;
        private readonly Action _onGameOver;

        private Enums.GameState _gameState;
        public Enums.GameState GameState
        {
            get => _gameState;
            set
            {
                _gameState = value;
                OnPropertyChanged();
                OnGameStateChanged(value);
            }
        }

        private int _heroHealth;
        public int HeroHealth
        {
            get => _heroHealth;
            set
            {
                _heroHealth = value;
                OnPropertyChanged();
            }
        }

        private bool _hasKey;
        public bool HasKey
        {
            get => _hasKey;
            set
            {
                _hasKey = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Cell> Cells { get; set; }
        public ObservableCollection<GameObject> GameObjects { get; set; }
        public ObservableCollection<bool> Hearts { get; set; }

        public GameViewModel(Action onWin, Action onGameOver)
        {
            _onWin = onWin;
            _onGameOver = onGameOver;
            _mazeGenerator = new MazeGenerator();
            _collisionService = new CollisionService();
            GameObjects = new ObservableCollection<GameObject>();
            Cells = new ObservableCollection<Cell>();
            Hearts = new ObservableCollection<bool>();

            _maze = _mazeGenerator.Generate();
            _gameEngine = new GameEngine(_maze, UpdateGame);

            HeroHealth = _maze.Hero.Health;
            HasKey = _maze.Hero.HasKey;
            _gameState = Enums.GameState.Playing;

            UpdateGameState();
            UpdateGame();
            _gameEngine.Start();
        }

        private void OnGameStateChanged(Enums.GameState state)
        {
            if (state == Enums.GameState.Win)
            {
                _gameEngine.Stop();
                _onWin();
            }
            else if (state == Enums.GameState.GameOver)
            {
                _gameEngine.Stop();
                _onGameOver();
            }
        }

        public void MoveHero(System.Windows.Input.Key key)
        {
            if (GameState != Enums.GameState.Playing)
            {
                return;
            }

            int dx = 0;
            int dy = 0;

            if (key == System.Windows.Input.Key.Up) dy = -1;
            else if (key == System.Windows.Input.Key.Down) dy = 1;
            else if (key == System.Windows.Input.Key.Left) dx = -1;
            else if (key == System.Windows.Input.Key.Right) dx = 1;
            else return;

            int newX = _maze.Hero.X + dx;
            int newY = _maze.Hero.Y + dy;

            if (!_maze.IsPassable(newX, newY))
            {
                return;
            }

            _maze.Hero.Move(dx, dy);

            CheckItemInteraction();
            CheckEnemyInteraction();
            UpdateGameState();
            UpdateGame();

            if (_gameState == Enums.GameState.Win)
            {
                OnGameStateChanged(Enums.GameState.Win);
            }
            else if (_gameState == Enums.GameState.GameOver)
            {
                OnGameStateChanged(Enums.GameState.GameOver);
            }
        }

        private void CheckItemInteraction()
        {
            Cell cell = _maze.Grid[_maze.Hero.Y, _maze.Hero.X];

            if (cell.Item != null && cell.Item.IsActive)
            {
                if (cell.Item is Portal)
                {
                    if (_maze.Hero.HasKey)
                    {
                        _gameState = Enums.GameState.Win; 
                        OnPropertyChanged(nameof(GameState));
                    }
                    return;
                }

                if (cell.Item is Key)
                {
                    cell.Item.Interact(_maze.Hero);
                    SpawnPortal();
                    cell.Item = null;
                    return;
                }

                cell.Item.Interact(_maze.Hero);
                cell.Item = null;
            }
        }

        private void SpawnPortal()
        {
            List<Floor> freeCells = new List<Floor>();

            for (int y = 0; y < _maze.Rows; y++)
            {
                for (int x = 0; x < _maze.Columns; x++)
                {
                    if (_maze.Grid[y, x] is Floor floor
                && floor.Item == null
                && !(_maze.Hero.X == x && _maze.Hero.Y == y)
                && !(_maze.Enemies.Exists(e => e.X == x && e.Y == y))
                && !(_maze.Ghosts.Exists(g => g.X == x && g.Y == y)))
                    {
                        freeCells.Add(floor);
                    }
                }
            }

            if (freeCells.Count == 0) return;

            Random random = new Random();
            Floor portalCell = freeCells[random.Next(freeCells.Count)];
            
                Portal portal = new Portal();
                portal.X = portalCell.X;
                portal.Y = portalCell.Y;
                portalCell.Item = portal;
                _maze.Items.Add(portal);
        }

        private void CheckEnemyInteraction()
        {
            bool collision = _collisionService.CheckHeroVsEnemy(_maze.Hero, _maze.Enemies);

            if (!collision)
            {
                collision = _collisionService.CheckHeroVsGhost(_maze.Hero, _maze.Ghosts, _maze);
            }

            if (collision)
            {
                _collisionService.SpawnHeart(_maze);
            }
        }

        private void UpdateGameState()
        {
            HeroHealth = _maze.Hero.Health;
            HasKey = _maze.Hero.HasKey;

            Hearts.Clear();
            for (int i = 0; i < _maze.Hero.Health; i++)
            {
                Hearts.Add(true);
            }

            if (_maze.Hero.Health <= 0)
            {
                GameState = Enums.GameState.GameOver;
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
                    Cell cell = _maze.Grid[y, x];
                    Cells.Add(cell);

                    if (cell.Item != null && cell.Item.IsActive)
                    {
                        GameObjects.Add(cell.Item);
                    }
                }
            }

            foreach (StandingEnemy enemy in _maze.Enemies)
            {
                if (enemy.IsActive)
                {
                    GameObjects.Add(enemy);
                }
            }

            foreach (Ghost ghost in _maze.Ghosts)
            {
                GameObjects.Add(ghost);
            }

            GameObjects.Add(_maze.Hero);
        }
    }
}