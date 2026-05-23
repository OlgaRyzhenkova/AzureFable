using System;

namespace AzureFable.Models
{
    internal class GameSettings
    {
        public GameDifficulty Difficulty { get; set; } = GameDifficulty.Normal;

        public TimeSpan EnemyMoveInterval
        {
            get
            {
                return Difficulty switch
                {
                    GameDifficulty.Easy => TimeSpan.FromMilliseconds(700),
                    GameDifficulty.Hard => TimeSpan.FromMilliseconds(300),
                    _ => TimeSpan.FromMilliseconds(500)
                };
            }
        }
    }
}
