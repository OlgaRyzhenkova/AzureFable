using System;
using System.Collections.Generic;

namespace AzureFable.Models
{
    internal abstract class Enemy : Unit
    {
        protected Enemy() : base(1)
        {
        }

        public abstract void Move(IMaze maze, Random random);

        public abstract bool Interact(Hero hero, IMaze maze, Random random);

        protected bool TryMove(IMaze maze, int dx, int dy)
        {
            int newX = X + dx;
            int newY = Y + dy;

            if (!maze.CanEnter(newX, newY, this))
            {
                return false;
            }

            MoveBy(dx, dy);
            return true;
        }

        protected static List<(int dx, int dy)> GetShuffledDirections(Random random)
        {
            List<(int dx, int dy)> directions = new List<(int dx, int dy)>
            {
                (0, -1),
                (0, 1),
                (-1, 0),
                (1, 0)
            };

            for (int i = directions.Count - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);
                var temp = directions[i];
                directions[i] = directions[j];
                directions[j] = temp;
            }

            return directions;
        }
    }
}
