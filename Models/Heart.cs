namespace AzureFable.Models
{
    internal class Heart : Item
    {
        public Heart()
        {
            Name = "Heart";
            ImagePath = "/Assets/Heart.png";
        }

        public override ItemInteractionResult Interact(Hero hero)
        {
            hero.Heal(1);
            Deactivate();
            return ItemInteractionResult.Healed;
        }
    }
}
