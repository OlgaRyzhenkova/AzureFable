using GameStateEnum = AzureFable.Models.GameState;

namespace AzureFable.Services
{
    internal interface IGameEngine
    {
        GameStateEnum GameState { get; }

        void Start();

        void Stop();

        void Pause();

        void Resume();

        void MoveHero(int dx, int dy);
    }
}
