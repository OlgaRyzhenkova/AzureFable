using System;

namespace AzureFable.Models
{
    internal abstract class Unit : GameObject
    {
        public int Health { get; private set; }
        public int MaxHealth { get; }
        public bool IsAlive => Health > 0;

        protected Unit(int maxHealth)
        {
            if (maxHealth <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(maxHealth));
            }

            MaxHealth = maxHealth;
            Health = maxHealth;
        }

        public void TakeDamage(int amount)
        {
            if (amount <= 0 || !IsActive)
            {
                return;
            }

            Health -= amount;
            if (Health <= 0)
            {
                Health = 0;
                Deactivate();
            }
        }
        public void Heal(int amount)
        {
            if (amount <= 0 || !IsActive)
            {
                return;
            }

            Health += amount;
            if (Health > MaxHealth)
            {
                Health = MaxHealth;
            }
        }
    }
}
