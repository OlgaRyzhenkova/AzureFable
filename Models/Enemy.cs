using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureFable.Models
{
    internal abstract class Enemy : Unit
    {
        public Enums.AIBehaviour Behaviour { get; protected set; }
        protected Enemy() : base(1)
        {
        }
    }
}
