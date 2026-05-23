using System;

namespace AzureFable.Models
{
    internal class Hero : Unit
    {
        public bool HasKey { get; private set; } = false;

        public Hero() : base(3)
        {
            Name = "Hero";
            ImagePath = "/Assets/Hero.png";
        }

        public void Move(int dx, int dy)
        {
            if (Math.Abs(dx) + Math.Abs(dy) != 1)
            {
                throw new ArgumentException("Hero can move only one cell horizontally or vertically.");
            }

            MoveBy(dx, dy);
        }

        public void CollectKey()
        {
            HasKey = true;
        }
    }
}
