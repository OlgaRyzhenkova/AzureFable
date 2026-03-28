using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureFable.Models
{
    internal class StandingEnemy : Enemy
    {
        public StandingEnemy()
        {
            Name = "StandingEnemy";
            ImagePath = "/Assets/Enemy.png";
            Behaviour = Enums.AIBehaviour.Standing;
        }
    }
}
