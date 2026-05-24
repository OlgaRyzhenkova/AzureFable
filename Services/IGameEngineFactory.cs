using AzureFable.Models;
using System;

namespace AzureFable.Services
{
    internal interface IGameEngineFactory
    {
        IGameEngine Create(IMaze maze, Action onUpdate, TimeSpan enemyMoveInterval);
    }
}
