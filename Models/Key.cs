using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureFable.Models
{
    internal class Key : Item
    {
        public Key()
        {
            Name = "Key";
            ImagePath = "Assets/Key.png";
        }
        public override void Interact(Hero hero)
        {
            hero.HasKey = true;
            IsActive = false;
        }
    }
}
