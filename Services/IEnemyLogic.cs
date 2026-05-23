using AzureFable.Models;

namespace AzureFable.Services
{
    internal interface IEnemyLogic
    {
        void MoveEnemies(IReadOnlyList<Enemy> enemies, Maze maze);
    }
}
