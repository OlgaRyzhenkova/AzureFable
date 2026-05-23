using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureFable.Models
{
    internal class Portal : Item
    {
        public Portal()
        {
            Name = "Portal";
            ImagePath = "/Assets/Portal.png";
        }

        public override ItemInteractionResult Interact(Hero hero)
        {
            if (hero.HasKey)
            {
                Deactivate();
                return ItemInteractionResult.Win;
            }

            return ItemInteractionResult.None;
        }
    }
}
