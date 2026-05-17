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
            ImagePath = "/Assets/Key.png";
        }
        public override Enums.ItemInteractionResult Interact(Hero hero)
        {
            hero.CollectKey();
            Deactivate();
            return Enums.ItemInteractionResult.KeyCollected;
        }
    }
}
