using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureFable.Models
{
    internal abstract class Unit : GameObject
    {
        public int Health { get; set; }
        public int MaxHealth { get; set; }

        public void TakeDamage(int amount)
        {
            Health -= amount;
            if (Health <= 0)
            {
                Health = 0;
                IsActive = false;
            }
        }
        public void Heal(int amount)
        {
            Health += amount;
            if (Health > MaxHealth)
            {
                Health = MaxHealth;
            }
        }
    }
}
