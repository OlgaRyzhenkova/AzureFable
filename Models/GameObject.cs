using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureFable.Models
{
    internal abstract class GameObject
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public bool IsActive { get; private set; } = true;
        public string Name { get; protected set; } = string.Empty;
        public string ImagePath { get; protected set; } = string.Empty;

        public void SetPosition(int x, int y)
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
        }

        public void MoveBy(int dx, int dy)
        {
            SetPosition(X + dx, Y + dy);
        }

        public void Deactivate()
        {
            IsActive = false;
        }
    }
}
