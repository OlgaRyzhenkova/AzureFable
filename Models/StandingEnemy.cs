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
            Behaviour = AIBehaviour.Standing;
        }

        public override void Move(Maze maze, Random random)
        {
        }

        public override bool Interact(Hero hero, Maze maze)
        {
            hero.TakeDamage(1);
            Deactivate();
            return true;
        }
    }
}
