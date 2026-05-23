namespace AzureFable.Models
{
    internal abstract class Item : GameObject, IInteractable
    {
        public abstract ItemInteractionResult Interact(Hero hero);
    }
}
