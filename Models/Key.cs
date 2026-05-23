namespace AzureFable.Models
{
    internal class Key : Item
    {
        public Key()
        {
            Name = "Key";
            ImagePath = "/Assets/Key.png";
        }
        public override ItemInteractionResult Interact(Hero hero)
        {
            hero.CollectKey();
            Deactivate();
            return ItemInteractionResult.KeyCollected;
        }
    }
}
