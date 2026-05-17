using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureFable.Models
{
    internal abstract class Cell
    {
        public int X { get; }
        public int Y { get; }
        public Item? Item { get; private set; }
        public string ImagePath { get; protected set; } = string.Empty;

        protected Cell(int x, int y)
        {
            if (x < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(x));
            }

            if (y < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(y));
            }

            X = x;
            Y = y;
            Item = null;
        }

        public abstract bool IsPassable();

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
