using System;

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

        public override bool Interact(Hero hero, Maze maze, Random random)
        {
            hero.TakeDamage(1);
            Flee(maze, random);
            return true;
        }

        private void Flee(Maze maze, Random random)
        {
            foreach (var direction in GetShuffledDirections(random))
            {
                if (TryMove(maze, direction.dx, direction.dy))
                {
                    return;
                }
            }
        }
    }
}
