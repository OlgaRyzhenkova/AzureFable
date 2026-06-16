using AzureFable.Models;
using System;
using System.Windows.Threading;
using GameStateEnum = AzureFable.Models.GameState;

namespace AzureFable.Services
{
    internal class GameEngine : IGameEngine
    {
        private readonly DispatcherTimer _timer;
        private readonly IEnemyLogic _enemyLogic;
        private readonly ICollisionService _collisionService;
        private readonly IItemSpawnService _itemSpawnService;
        private readonly Action _onUpdate;
        private IMaze _maze;

        public GameStateEnum GameState { get; private set; }

        public GameEngine(
            IMaze maze,
            Action onUpdate,
            TimeSpan enemyMoveInterval,
            IEnemyLogic enemyLogic,
            ICollisionService collisionService,
            IItemSpawnService itemSpawnService)
        {
            ArgumentNullException.ThrowIfNull(maze);
            ArgumentNullException.ThrowIfNull(onUpdate);
            ArgumentNullException.ThrowIfNull(enemyLogic);
            ArgumentNullException.ThrowIfNull(collisionService);
            ArgumentNullException.ThrowIfNull(itemSpawnService);

            _maze = maze;
            _onUpdate = onUpdate;
            _enemyLogic = enemyLogic;
            _collisionService = collisionService;
            _itemSpawnService = itemSpawnService;
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

        private void CheckItemInteraction()
        {
            Cell cell = _maze.GetCell(_maze.Hero.X, _maze.Hero.Y);
            IInteractable? interactable = cell.Item;

            if (interactable == null || !interactable.IsActive)
            {
                return;
            }

            ItemInteractionResult result = interactable.Interact(_maze.Hero);

            switch (result)
            {
                case ItemInteractionResult.KeyCollected:
                    _itemSpawnService.SpawnPortal(_maze);
                    break;
                case ItemInteractionResult.Win:
                    SetGameState(GameStateEnum.Win);
                    break;
            }

            if (!interactable.IsActive)
            {
                cell.RemoveItem();
            }
        }

        private void CheckEnemyInteraction()
        {
            bool collision = _collisionService.CheckHeroVsEnemies(_maze.Hero, _maze.Enemies, _maze);

            if (collision && _maze.Hero.IsAlive)
            {
                _itemSpawnService.SpawnHeart(_maze);
            }
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
