using AzureFable.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureFable.Services
{
    internal class EnemyLogic : IEnemyLogic
    {
        private readonly Random _random = new Random();

        public void MoveEnemies(IReadOnlyList<Enemy> enemies, Maze maze)
        {
            foreach (Enemy enemy in enemies)
            {
                if (enemy.IsActive)
                {
                    enemy.Move(maze, _random);
                }
            }
        }
    }
}
