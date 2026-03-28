using AzureFable.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureFable.Services
{
    internal class EnemyLogic
    {
        private readonly Random _random = new Random();

        public void MoveGhosts(List<Ghost> ghosts, Maze maze)
        {
            foreach (Ghost ghost in ghosts)
            {
                 MoveGhost(ghost, maze);
            }
        }

        private void MoveGhost(Ghost ghost, Maze maze)
        {
            List<(int dx, int dy)> directions = new List<(int dx, int dy)>
            {
                (0, -1),
                (0, 1),
                (-1, 0),
                (1, 0)
            };

            Shuffle(directions);

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

        private void Shuffle(List<(int dx, int dy)> list)
        {
            for (int i = list.Count - 1; i > 0; i--)
            {
                int j = _random.Next(i + 1);
                var temp = list[i];
                list[i] = list[j];
                list[j] = temp;
            }
        }
    }
}
