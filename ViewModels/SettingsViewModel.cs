using System;
using AzureFable.Models;

namespace AzureFable.ViewModels
{
    internal class SettingsViewModel : ViewModelBase
    {
        private readonly GameSettings _settings;
        private readonly Action _onBack;

        public RelayCommand EasyCommand { get; }
        public RelayCommand NormalCommand { get; }
        public RelayCommand HardCommand { get; }
        public RelayCommand BackCommand { get; }

        public GameDifficulty Difficulty
        {
            get => _settings.Difficulty;
            private set
            {
                if (_settings.Difficulty == value)
                {
                    return;
                }

                _settings.ChangeDifficulty(value);
                OnPropertyChanged();
                OnPropertyChanged(nameof(DifficultyText));
            }
        }

        public string DifficultyText
        {
            get
            {
                return Difficulty switch
                {
                    GameDifficulty.Easy => "Легка",
                    GameDifficulty.Hard => "Важка",
                    _ => "Звичайна"
                };
            }
        }

        public SettingsViewModel(GameSettings settings, Action onBack)
        {
            _settings = settings;
            _onBack = onBack;

            EasyCommand = new RelayCommand(() => Difficulty = GameDifficulty.Easy);
            NormalCommand = new RelayCommand(() => Difficulty = GameDifficulty.Normal);
            HardCommand = new RelayCommand(() => Difficulty = GameDifficulty.Hard);
            BackCommand = new RelayCommand(_onBack);
        }
    }
}
