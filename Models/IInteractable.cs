namespace AzureFable.Models
{
    internal interface IInteractable
    {
        Enums.ItemInteractionResult Interact(Hero hero);
    }
}
