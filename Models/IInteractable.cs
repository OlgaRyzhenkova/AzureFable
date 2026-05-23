namespace AzureFable.Models
{
    internal interface IInteractable
    {
        bool IsActive { get; }

        ItemInteractionResult Interact(Hero hero);
    }
}
