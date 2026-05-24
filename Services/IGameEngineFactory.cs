using AzureFable.Models;
using System;

namespace AzureFable.Services
{
    internal interface IGameEngineFactory
    {
        IGameEngine Create(Maze maze, Action onUpdate, TimeSpan enemyMoveInterval);
    }
}
