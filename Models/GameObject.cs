using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureFable.Models
{
    internal abstract class GameObject
    {
        public int X { get; set; }
        public int Y { get; set; }
        public bool IsActive { get; set; } = true;
        public string Name { get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty;
    }
}
