using AzureFable.Models;
using System.Collections.Generic;

namespace AzureFable.Services
{
    internal static class DefaultEnemySpawnRules
    {
        public static IReadOnlyList<EnemySpawnRule> Create()
        {
            return new List<EnemySpawnRule>
            {
                new EnemySpawnRule(3, () => new StandingEnemy()),
                new EnemySpawnRule(2, () => new Ghost())
            };
        }
    }
}
