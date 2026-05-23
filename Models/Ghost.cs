using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureFable.Models
{
    internal class Ghost : Enemy
    {
        public Ghost()
        {
            Name = "Ghost";
            ImagePath = "/Assets/Ghost.png";
            Behaviour = AIBehaviour.Random;
        }

        public override void Move(Maze maze, Random random)
        {
            foreach (var direction in GetShuffledDirections(random))
            {
                if (TryMove(maze, direction.dx, direction.dy))
                {
                    return;
                }
            }
        }

        public override bool Interact(Hero hero, Maze maze)
        {
            hero.TakeDamage(1);
            Flee(maze);
            return true;
        }

        private void Flee(Maze maze)
        {
            List<(int dx, int dy)> directions = new List<(int dx, int dy)>
            {
                (0, -1),
                (0, 1),
                (-1, 0),
                (1, 0)
            };

            foreach (var direction in directions)
            {
                if (TryMove(maze, direction.dx, direction.dy))
                {
                    return;
                }
            }
        }
    }
}
