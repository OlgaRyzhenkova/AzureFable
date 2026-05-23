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
            foreach (var direction in GetShuffledDirections(Random.Shared))
            {
                if (TryMove(maze, direction.dx, direction.dy))
                {
                    return;
                }
            }
        }
    }
}
