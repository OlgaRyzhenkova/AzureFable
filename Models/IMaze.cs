using System.Collections.Generic;

namespace AzureFable.Models
{
    internal interface IMaze
    {
        Hero Hero { get; }
        IReadOnlyList<Enemy> Enemies { get; }
        IReadOnlyList<Item> Items { get; }
        int Rows { get; }
        int Columns { get; }

        Cell GetCell(int x, int y);

        void AddItem(Item item);

        bool IsPassable(int x, int y);

        bool CanEnter(int x, int y, Unit? unit);
    }
}
