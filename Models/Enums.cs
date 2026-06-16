namespace AzureFable.Models
{
    internal enum GameState
    {
        Playing,
        Paused,
        GameOver,
        Win
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
