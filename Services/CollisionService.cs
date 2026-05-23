using AzureFable.Models;

namespace AzureFable.Services
{
    internal class CollisionService : ICollisionService
    {
        private readonly Random _random = new Random();

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

        public void SpawnHeart(Maze maze)
        {
            List<Floor> freeCells = new List<Floor>();

            for (int y = 0; y < maze.Rows; y++)
            {
                for (int x = 0; x < maze.Columns; x++)
                {
                    if (maze.GetCell(x, y) is Floor floor && floor.Item == null
                        && !(maze.Hero.X == x && maze.Hero.Y == y))
                    {
                        freeCells.Add(floor);
                    }
                }
            }

            if (freeCells.Count == 0)
            {
                return;
            }

            int index = _random.Next(freeCells.Count);
            Floor cell = freeCells[index];
            Heart heart = new Heart();
            cell.PlaceItem(heart);
            maze.AddItem(heart);
        }
    }
}
