using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureFable.Models
{
    internal abstract class Item : GameObject
    {
        public abstract void Interact(Hero hero);
    }
}
