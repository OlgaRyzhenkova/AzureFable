using System;

namespace AzureFable.Models
{
    internal class GameSettings
    {
        public Enums.GameDifficulty Difficulty { get; set; } = Enums.GameDifficulty.Normal;

        public TimeSpan EnemyMoveInterval
        {
            get
            {
                return Difficulty switch
                {
                    Enums.GameDifficulty.Easy => TimeSpan.FromMilliseconds(700),
                    Enums.GameDifficulty.Hard => TimeSpan.FromMilliseconds(300),
                    _ => TimeSpan.FromMilliseconds(500)
                };
            }
        }
    }
}
