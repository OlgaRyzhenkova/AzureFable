using System;

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

        public override bool Interact(Hero hero, Maze maze, Random random)
        {
            hero.TakeDamage(1);
            Deactivate();
            return true;
        }
    }
}
