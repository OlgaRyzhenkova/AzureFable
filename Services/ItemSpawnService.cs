using AzureFable.Models;

namespace AzureFable.Services
{
    internal class ItemSpawnService : IItemSpawnService
    {
        private readonly Random _random = new Random();

        public void SpawnPortal(Maze maze)
        {
            ArgumentNullException.ThrowIfNull(maze);

            List<Floor> freeCells = new List<Floor>();

            for (int y = 0; y < maze.Rows; y++)
            {
                for (int x = 0; x < maze.Columns; x++)
                {
                    if (maze.GetCell(x, y) is Floor floor
                        && floor.Item == null
                        && !(maze.Hero.X == x && maze.Hero.Y == y)
                        && !maze.Enemies.Any(e => e.X == x && e.Y == y))
                    {
                        freeCells.Add(floor);
                    }
                }
            }

            if (freeCells.Count == 0)
            {
                return;
            }

            Floor portalCell = freeCells[_random.Next(freeCells.Count)];
            Portal portal = new Portal();
            portalCell.PlaceItem(portal);
            maze.AddItem(portal);
        }
    }
}
