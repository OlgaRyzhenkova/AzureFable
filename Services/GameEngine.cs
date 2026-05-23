using AzureFable.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using GameStateEnum = AzureFable.Models.GameState;

namespace AzureFable.Services
{
    internal class GameEngine
    {
        private readonly DispatcherTimer _timer;
        private readonly EnemyLogic _enemyLogic;
        private readonly CollisionService _collisionService;
        private readonly Random _random;
        private Maze _maze;
        private Action _onUpdate;

        public GameStateEnum GameState { get; private set; }

        public GameEngine(Maze maze, Action onUpdate, TimeSpan enemyMoveInterval)
        {
            _maze = maze;
            _onUpdate = onUpdate;
            _enemyLogic = new EnemyLogic();
            _collisionService = new CollisionService();
            _random = new Random();
            GameState = GameStateEnum.Playing;

            _timer = new DispatcherTimer();
            _timer.Interval = enemyMoveInterval;
            _timer.Tick += OnTick;
        }

        public void Start()
        {
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
        }

        public void Pause()
        {
            if (GameState != GameStateEnum.Playing)
            {
                return;
            }

            GameState = GameStateEnum.Paused;
            Stop();
        }

        public void Resume()
        {
            if (GameState != GameStateEnum.Paused)
            {
                return;
            }

            GameState = GameStateEnum.Playing;
            Start();
        }

        private void OnTick(object? sender, EventArgs e)
        {
            if (GameState != GameStateEnum.Playing)
            {
                return;
            }

            _enemyLogic.MoveEnemies(_maze.Enemies, _maze);
            CheckEnemyInteraction();
            UpdateGameState();

            _onUpdate();
        }

        public void MoveHero(int dx, int dy)
        {
            if (GameState != GameStateEnum.Playing)
            {
                return;
            }

            int newX = _maze.Hero.X + dx;
            int newY = _maze.Hero.Y + dy;

            if (!_maze.CanEnter(newX, newY, _maze.Hero))
            {
                return;
            }

            _maze.Hero.Move(dx, dy);

            CheckItemInteraction();

            if (GameState == GameStateEnum.Playing)
            {
                CheckEnemyInteraction();
                UpdateGameState();
            }

            _onUpdate();
        }

        public void UpdateMaze(Maze maze)
        {
            _maze = maze;
            GameState = GameStateEnum.Playing;
        }

        private void CheckItemInteraction()
        {
            Cell cell = _maze.GetCell(_maze.Hero.X, _maze.Hero.Y);
            Item? item = cell.Item;

            if (item == null || !item.IsActive)
            {
                return;
            }

            ItemInteractionResult result = item.Interact(_maze.Hero);

            switch (result)
            {
                case ItemInteractionResult.KeyCollected:
                    SpawnPortal();
                    break;
                case ItemInteractionResult.Win:
                    SetGameState(GameStateEnum.Win);
                    break;
            }

            if (!item.IsActive)
            {
                cell.RemoveItem();
            }
        }

        private void CheckEnemyInteraction()
        {
            bool collision = _collisionService.CheckHeroVsEnemies(_maze.Hero, _maze.Enemies, _maze);

            if (collision && _maze.Hero.IsAlive)
            {
                _collisionService.SpawnHeart(_maze);
            }
        }

        private void SpawnPortal()
        {
            List<Floor> freeCells = new List<Floor>();

            for (int y = 0; y < _maze.Rows; y++)
            {
                for (int x = 0; x < _maze.Columns; x++)
                {
                    if (_maze.GetCell(x, y) is Floor floor
                        && floor.Item == null
                        && !(_maze.Hero.X == x && _maze.Hero.Y == y)
                        && !_maze.Enemies.Any(e => e.X == x && e.Y == y))
                    {
                        freeCells.Add(floor);
                    }
                }
            }

            if (freeCells.Count == 0)
            {
                return;
            }

            Floor portalCell = freeCells[_random.Next(freeCells.Count)];
            Portal portal = new Portal();
            portalCell.PlaceItem(portal);
            _maze.AddItem(portal);
        }

        private void UpdateGameState()
        {
            if (_maze.Hero.Health <= 0)
            {
                SetGameState(GameStateEnum.GameOver);
            }
        }

        private void SetGameState(GameStateEnum state)
        {
            if (GameState == state)
            {
                return;
            }

            GameState = state;

            if (state == GameStateEnum.Win || state == GameStateEnum.GameOver)
            {
                Stop();
            }
        }
    }
}
