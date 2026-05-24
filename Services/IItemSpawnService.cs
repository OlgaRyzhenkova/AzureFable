using AzureFable.Models;

namespace AzureFable.Services
{
    internal interface IItemSpawnService
    {
        void SpawnHeart(IMaze maze);

        void SpawnPortal(IMaze maze);
    }
}
