using AzureFable.Models;

namespace AzureFable.Services
{
    internal class CollisionService : ICollisionService
    {
        public bool CheckHeroVsEnemies(Hero hero, IReadOnlyList<Enemy> enemies, Maze maze)
        {
            foreach (Enemy enemy in enemies)
            {
                if (enemy.IsActive && enemy.X == hero.X && enemy.Y == hero.Y)
                {
                    return enemy.Interact(hero, maze);
                }
            }
            return false;
        }
    }
}
