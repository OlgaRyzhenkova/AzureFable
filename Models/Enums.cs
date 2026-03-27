using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureFable.Models
{
    internal class Enums
    {
        public enum CellType
        {
            Floor,
            Wall
        }

        public enum GameState
        {
            Playing,
            Paused,
            GameOver,
            Win
        }
        public enum AIBehaviour
        {
            Standing,
            Random
        }
    }
}
