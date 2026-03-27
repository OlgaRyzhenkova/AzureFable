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
            ImagePath = "Assets/Ghost.png";
            Behaviour = Enums.AIBehaviour.Random;
        }
        public void Flee(int dx, int dy)
        {
            X += dx;
            Y += dy;
        }
    }
}
