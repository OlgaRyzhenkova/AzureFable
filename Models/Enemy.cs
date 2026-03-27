using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureFable.Models
{
    internal abstract class Enemy : Unit
    {
        public Enums.AIBehaviour Behaviour { get; set; }
        protected Enemy()
        {
            MaxHealth = 1;
            Health = 1;
        }
    }
}
