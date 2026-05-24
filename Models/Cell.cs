using System;

namespace AzureFable.Models
{
    internal abstract class Cell : GameObject
    {
        public Item? Item { get; private set; }

        protected Cell(int x, int y)
        {
            SetPosition(x, y);
            Item = null;
        }

        public abstract bool CanEnter(Unit? unit);

        public bool IsPassable()
        {
            return CanEnter(null);
        }

        public void PlaceItem(Item item)
        {
            ArgumentNullException.ThrowIfNull(item);

            Item = item;
            item.SetPosition(X, Y);
        }

        public void RemoveItem()
        {
            Item = null;
        }
    }
}
