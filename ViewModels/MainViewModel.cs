using System;

namespace AzureFable.ViewModels
{
    internal class MainViewModel : ViewModelBase
    {
        private readonly Action<ViewModelBase> _showView;
        private readonly Action<bool> _showGameOver;
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

        public MainViewModel(Action<ViewModelBase> showView, Action<bool> showGameOver)
        {
            _showView = showView;
            _showGameOver = showGameOver;
            _menuViewModel = new MenuViewModel(StartGame);
            _currentView = _menuViewModel;
        }

        public void StartGame()
        {
            _gameViewModel = new GameViewModel(
                () => _showGameOver(true),
                () => _showGameOver(false)
            );
            CurrentView = _gameViewModel;
        }

        public void ShowMenu()
        {
            CurrentView = _menuViewModel;
        }
    }
}