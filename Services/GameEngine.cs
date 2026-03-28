using AzureFable.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace AzureFable.Services
{
    internal class GameEngine
    {
        private readonly DispatcherTimer _timer;
        private readonly EnemyLogic _enemyLogic;
        private readonly CollisionService _collisionService;
        private Maze _maze;
        private Action _onUpdate;

        public GameEngine(Maze maze, Action onUpdate)
        {
            _maze = maze;
            _onUpdate = onUpdate;
            _enemyLogic = new EnemyLogic();
            _collisionService = new CollisionService();

            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromMilliseconds(200);
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

        private void OnTick(object? sender, EventArgs e)
        {
            _enemyLogic.MoveGhosts(_maze.Ghosts, _maze);

            bool collision = _collisionService.CheckHeroVsEnemy(_maze.Hero, _maze.Enemies);

            if (!collision)
            {
                collision = _collisionService.CheckHeroVsGhost(_maze.Hero, _maze.Ghosts, _maze);
            }

            if (collision)
            {
                _collisionService.SpawnHeart(_maze);
            }

            _onUpdate();
        }

        public void UpdateMaze(Maze maze)
        {
            _maze = maze;
        }
    }
}
