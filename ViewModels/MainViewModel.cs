using System;

namespace AzureFable.ViewModels
{
    using AzureFable.Models;
    using AzureFable.Services;

    internal class MainViewModel : ViewModelBase
    {
        private readonly Action<bool> _showGameOver;
        private readonly Action _showPause;
        private readonly Action _showGameHelp;
        private readonly Action _showExitConfirmation;
        private ViewModelBase _currentView;

        public ViewModelBase CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        private readonly MenuViewModel _menuViewModel;
        private GameViewModel? _gameViewModel;
        private readonly GameSettings _settings;

        public MainViewModel(Action<bool> showGameOver, Action showPause, Action showGameHelp, Action showExitConfirmation)
        {
            _showGameOver = showGameOver;
            _showPause = showPause;
            _showGameHelp = showGameHelp;
            _showExitConfirmation = showExitConfirmation;
            _settings = new GameSettings();
            _menuViewModel = new MenuViewModel(StartGame, ShowSettings, ShowHelp, ConfirmExit);
            _currentView = _menuViewModel;
        }

        public void StartGame()
        {
            _gameViewModel = new GameViewModel(
                () => _showGameOver(true),
                () => _showGameOver(false),
                _showPause,
                _showGameHelp,
                _settings,
                new EnemyLogic(),
                new CollisionService()
            );
            CurrentView = _gameViewModel;
        }

        public void ResumeGame()
        {
            if (_gameViewModel == null)
            {
                return;
            }

            _gameViewModel.Resume();
        }

        public void ShowMenu()
        {
            CurrentView = _menuViewModel;
        }

        public void ShowHelp()
        {
            CurrentView = new HelpViewModel(ShowMenu);
        }

        public void ShowSettings()
        {
            CurrentView = new SettingsViewModel(_settings, ShowMenu);
        }

        public void ConfirmExit()
        {
            _showExitConfirmation();
        }
    }
}
