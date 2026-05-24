using AzureFable.Models;

namespace AzureFable.Services
{
    internal class CollisionService : ICollisionService
    {
        private readonly Random _random = new Random();

        public bool CheckHeroVsEnemies(Hero hero, IReadOnlyList<Enemy> enemies, IMaze maze)
        {
            bool hasCollision = false;

            foreach (Enemy enemy in enemies)
            {
                if (enemy.IsActive && enemy.X == hero.X && enemy.Y == hero.Y)
                {
                    hasCollision |= enemy.Interact(hero, maze, _random);
                }
            }

            return hasCollision;
        }
    }
}
