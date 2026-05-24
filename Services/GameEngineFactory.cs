using AzureFable.Models;
using System;

namespace AzureFable.Services
{
    internal class GameEngineFactory : IGameEngineFactory
    {
        private readonly IEnemyLogic _enemyLogic;
        private readonly ICollisionService _collisionService;
        private readonly IItemSpawnService _itemSpawnService;

        public GameEngineFactory(
            IEnemyLogic enemyLogic,
            ICollisionService collisionService,
            IItemSpawnService itemSpawnService)
        {
            ArgumentNullException.ThrowIfNull(enemyLogic);
            ArgumentNullException.ThrowIfNull(collisionService);
            ArgumentNullException.ThrowIfNull(itemSpawnService);

            _enemyLogic = enemyLogic;
            _collisionService = collisionService;
            _itemSpawnService = itemSpawnService;
        }

        public IGameEngine Create(IMaze maze, Action onUpdate, TimeSpan enemyMoveInterval)
        {
            return new GameEngine(
                maze,
                onUpdate,
                enemyMoveInterval,
                _enemyLogic,
                _collisionService,
                _itemSpawnService);
        }
    }
}
