using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureFable.Models
{
    internal class Maze
    {
        public Cell[,] Grid { get; set; }
        public Hero Hero { get; set; }
        public List<StandingEnemy> Enemies { get; set; }
        public List<Ghost> Ghosts { get; set; }
        public List<Item> Items { get; set; }
        public int Rows { get; private set; }
        public int Columns { get; private set; }

        public Maze(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            Grid = new Cell[rows, columns];
            Enemies = new List<StandingEnemy>();
            Ghosts = new List<Ghost>();
            Items = new List<Item>();
            Hero = new Hero();
        }

        public bool IsPassable(int x, int y)
        {
            if (x < 0 || y < 0 || x >= Columns || y >= Rows)
            {
                return false;
            }

            return Grid[y, x].IsPassable();
        }
    }
}
