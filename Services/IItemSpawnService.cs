using AzureFable.Models;

namespace AzureFable.Services
{
    internal interface IItemSpawnService
    {
        void SpawnHeart(Maze maze);

        void SpawnPortal(Maze maze);
    }
}
