using System;

namespace AzureFable.ViewModels
{
    internal class MainViewModel : ViewModelBase
    {
        private readonly Action<ViewModelBase> _showView;
        private readonly Action<bool> _showGameOver;
        private readonly Action _showPause;
        private ViewModelBase _currentView;

        public ViewModelBase CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged();
                _showView(value);
            }
        }

        private readonly MenuViewModel _menuViewModel;
        private GameViewModel? _gameViewModel;

        public MainViewModel(Action<ViewModelBase> showView, Action<bool> showGameOver, Action showPause)
        {
            _showView = showView;
            _showGameOver = showGameOver;
            _showPause = showPause;
            _menuViewModel = new MenuViewModel(StartGame);
            _currentView = _menuViewModel;
        }

        public void StartGame()
        {
            _gameViewModel = new GameViewModel(
                () => _showGameOver(true),
                () => _showGameOver(false),
                _showPause
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
    }
}
