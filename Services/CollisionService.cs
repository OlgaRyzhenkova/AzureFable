using AzureFable.Models;

namespace AzureFable.Services
{
    internal class CollisionService
    {
        private readonly Random _random = new Random();

        public bool CheckHeroVsEnemy(Hero hero, List<StandingEnemy> enemies)
        {
            foreach (StandingEnemy enemy in enemies)
            {
                if (enemy.IsActive && enemy.X == hero.X && enemy.Y == hero.Y)
                {
                    hero.TakeDamage(1);
                    enemy.IsActive = false;
                    return true;
                }
            }
            return false;
        }

        public bool CheckHeroVsGhost(Hero hero, List<Ghost> ghosts, Maze maze)
        {
            foreach (Ghost ghost in ghosts)
            {
                if (ghost.IsActive && ghost.X == hero.X && ghost.Y == hero.Y)
                {
                    hero.TakeDamage(1);
                    FleeGhost(ghost, maze);
                    return true;
                }
            }
            return false;
        }

        private void FleeGhost(Ghost ghost, Maze maze)
        {
            List<(int dx, int dy)> directions = new List<(int dx, int dy)>
            {
                (0, -1),
                (0, 1),
                (-1, 0),
                (1, 0)
            };

            foreach (var dir in directions)
            {
                int newX = ghost.X + dir.dx;
                int newY = ghost.Y + dir.dy;

                if (maze.IsPassable(newX, newY))
                {
                    ghost.Flee(dir.dx, dir.dy);
                    return;
                }
            }
        }

        public void SpawnHeart(Maze maze)
        {
            List<Floor> freeCells = new List<Floor>();

            for (int y = 0; y < maze.Rows; y++)
            {
                for (int x = 0; x < maze.Columns; x++)
                {
                    if (maze.Grid[y, x] is Floor floor && floor.Item == null
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
            heart.X = cell.X;
            heart.Y = cell.Y;
            cell.Item = heart;
            maze.Items.Add(heart);
        }
    }
}
