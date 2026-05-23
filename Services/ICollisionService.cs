using AzureFable.Models;

namespace AzureFable.Services
{
    internal interface ICollisionService
    {
        bool CheckHeroVsEnemies(Hero hero, IReadOnlyList<Enemy> enemies, Maze maze);

        void SpawnHeart(Maze maze);
    }
}
