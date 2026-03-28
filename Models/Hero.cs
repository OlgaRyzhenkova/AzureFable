using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureFable.Models
{
    internal class Hero : Unit
    {
        public bool HasKey { get; set; } = false;

        public Hero()
        {
            Name = "Hero";
            MaxHealth = 3;
            Health = 3;
            ImagePath = "/Assets/Hero.png";
        }

        public void Move(int dx, int dy)
        {
            X += dx;
            Y += dy;
        }
    }
}
