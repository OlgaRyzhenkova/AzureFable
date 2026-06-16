using System;

namespace AzureFable.Models
{
    internal class StandingEnemy : Enemy
    {
        public StandingEnemy()
        {
            Name = "StandingEnemy";
            ImagePath = "/Assets/Enemy.png";
        }

        public override void Move(IMaze maze, Random random)
        {
        }

        public override bool Interact(Hero hero, IMaze maze, Random random)
        {
            hero.TakeDamage(1);
            Deactivate();
            return true;
        }
    }
}
