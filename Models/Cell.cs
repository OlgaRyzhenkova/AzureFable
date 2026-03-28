using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureFable.Models
{
    internal abstract class Cell
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Item? Item { get; set; }
        public string ImagePath { get; set; } = string.Empty;

        protected Cell(int x, int y)
        {
            X = x;
            Y = y;
            Item = null;
        }

        public abstract bool IsPassable();
    }
}
