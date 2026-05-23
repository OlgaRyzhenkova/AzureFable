namespace AzureFable.Models
{
    internal enum CellType
    {
        Floor,
        Wall
    }

    internal enum GameState
    {
        Playing,
        Paused,
        GameOver,
        Win
    }

    internal enum AIBehaviour
    {
        Standing,
        Random
    }

    internal enum ItemInteractionResult
    {
        None,
        KeyCollected,
        Healed,
        Win
    }

    internal enum GameDifficulty
    {
        Easy,
        Normal,
        Hard
    }
}
