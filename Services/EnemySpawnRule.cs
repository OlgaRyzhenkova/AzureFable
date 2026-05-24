using AzureFable.Models;
using System;

namespace AzureFable.Services
{
    internal class EnemySpawnRule
    {
        public int Count { get; }
        public Func<Enemy> CreateEnemy { get; }

        public EnemySpawnRule(int count, Func<Enemy> createEnemy)
        {
            if (count < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }

            ArgumentNullException.ThrowIfNull(createEnemy);

            Count = count;
            CreateEnemy = createEnemy;
        }
    }
}
