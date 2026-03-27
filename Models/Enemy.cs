using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureFable.Models
{
    internal class Enemy : Unit
    {
        public Enums.AIBehaviour Behaviour { get; set; }

        public Enemy()
        {
            Name = "Enemy";
            MaxHealth = 1;
            Health = 1;
            ImagePath = "Assets/Enemy.png";
            Behaviour = Enums.AIBehaviour.Standing;
        }
    }
}
