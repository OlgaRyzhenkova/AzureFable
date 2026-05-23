using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AzureFable.Models
{
    internal abstract class GameObject : INotifyPropertyChanged
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public bool IsActive { get; private set; } = true;
        public string Name { get; protected set; } = string.Empty;
        public string ImagePath { get; protected set; } = string.Empty;

        public event PropertyChangedEventHandler? PropertyChanged;

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

            if (X == x && Y == y)
            {
                return;
            }

            X = x;
            Y = y;
            OnPropertyChanged(nameof(X));
            OnPropertyChanged(nameof(Y));
        }

        public void MoveBy(int dx, int dy)
        {
            SetPosition(X + dx, Y + dy);
        }

        public void Deactivate()
        {
            if (!IsActive)
            {
                return;
            }

            IsActive = false;
            OnPropertyChanged(nameof(IsActive));
        }

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
