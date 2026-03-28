using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureFable.Models
{
    internal class Wall : Cell
    {
        public Wall(int x, int y) : base(x, y)
        {
            ImagePath = "Images/Wall.png";
        }

        public override bool IsPassable()
        {
            return false;
        }
    }
}
