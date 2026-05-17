using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            MoveBy(dx, dy);
        }

        public void CollectKey()
        {
            HasKey = true;
        }
    }
}
