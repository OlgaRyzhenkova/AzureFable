using System;

namespace AzureFable.Services
{
    internal class MazeGenerationException : Exception
    {
        public MazeGenerationException(string message) : base(message)
        {
        }
    }
}
