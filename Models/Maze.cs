using System;
using System.Collections.Generic;

namespace AzureFable.Models
{
    internal class Maze : IMaze
    {
        private readonly Cell[,] _grid;
        private readonly List<Enemy> _enemies;
        private readonly List<Item> _items;

        public Hero Hero { get; }
        public IReadOnlyList<Enemy> Enemies => _enemies;
        public IReadOnlyList<Item> Items => _items;
        public int Rows { get; private set; }
        public int Columns { get; private set; }

        public Maze(int rows, int columns, Hero hero)
        {
            if (rows <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(rows));
            }

            if (columns <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(columns));
            }

            ArgumentNullException.ThrowIfNull(hero);

            Rows = rows;
            Columns = columns;
            _grid = new Cell[rows, columns];
            _enemies = new List<Enemy>();
            _items = new List<Item>();
            Hero = hero;
        }

        public void SetCell(Cell cell)
        {
            ArgumentNullException.ThrowIfNull(cell);

            if (cell.X < 0 || cell.Y < 0 || cell.X >= Columns || cell.Y >= Rows)
            {
                throw new ArgumentOutOfRangeException(nameof(cell));
            }

            _grid[cell.Y, cell.X] = cell;
        }

        public Cell GetCell(int x, int y)
        {
            if (x < 0 || y < 0 || x >= Columns || y >= Rows)
            {
                throw new ArgumentOutOfRangeException();
            }

            return _grid[y, x];
        }

        public void AddEnemy(Enemy enemy)
        {
            ArgumentNullException.ThrowIfNull(enemy);

            _enemies.Add(enemy);
        }

        public void AddItem(Item item)
        {
            ArgumentNullException.ThrowIfNull(item);

            _items.Add(item);
        }

        public bool IsPassable(int x, int y)
        {
            return CanEnter(x, y, null);
        }

        public bool CanEnter(int x, int y, Unit? unit)
        {
            if (x < 0 || y < 0 || x >= Columns || y >= Rows)
            {
                return false;
            }

            return GetCell(x, y).CanEnter(unit);
        }
    }
}
