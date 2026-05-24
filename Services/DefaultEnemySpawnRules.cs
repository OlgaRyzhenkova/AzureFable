using AzureFable.Models;
using System.Collections.Generic;

namespace AzureFable.Services
{
    internal static class DefaultEnemySpawnRules
    {
        public static IReadOnlyList<EnemySpawnRule> Create()
        {
            return Create(GameDifficulty.Normal);
        }

        public static IReadOnlyList<EnemySpawnRule> Create(GameDifficulty difficulty)
        {
            return new List<EnemySpawnRule>
            {
                new EnemySpawnRule(GetStandingEnemyCount(difficulty), () => new StandingEnemy()),
                new EnemySpawnRule(GetGhostCount(difficulty), () => new Ghost())
            };
        }

        private static int GetStandingEnemyCount(GameDifficulty difficulty)
        {
            return difficulty switch
            {
                GameDifficulty.Easy => 2,
                GameDifficulty.Hard => 4,
                _ => 3
            };
        }

        private static int GetGhostCount(GameDifficulty difficulty)
        {
            return difficulty switch
            {
                GameDifficulty.Easy => 1,
                GameDifficulty.Hard => 3,
                _ => 2
            };
        }
    }
}
