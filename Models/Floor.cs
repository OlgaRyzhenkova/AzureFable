using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureFable.Models
{
    internal class Floor : Cell
    {
        public Floor(int x, int y) : base(x, y)
        {
            ImagePath = "Images/Floor.png";
        }

        public override bool IsPassable()
        {
            return true;
        }
    }
}
