using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureFable.Models
{
    internal class Heart : Item
    {
        public Heart()
        {
            Name = "Heart";
            ImagePath = "/Assets/Heart.png";
        }

        public override Enums.ItemInteractionResult Interact(Hero hero)
        {
            hero.Heal(1);
            Deactivate();
            return Enums.ItemInteractionResult.Healed;
        }
    }
}
