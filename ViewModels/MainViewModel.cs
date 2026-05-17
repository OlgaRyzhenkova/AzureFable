using System;

namespace AzureFable.ViewModels
{
    internal class MainViewModel : ViewModelBase
    {
        private readonly Action<ViewModelBase> _showView;
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
                _showView(value);
            }
        }

        private readonly MenuViewModel _menuViewModel;
        private GameViewModel? _gameViewModel;

        public MainViewModel(Action<ViewModelBase> showView, Action<bool> showGameOver, Action showPause, Action showGameHelp, Action showExitConfirmation)
        {
            _showView = showView;
            _showGameOver = showGameOver;
            _showPause = showPause;
            _showGameHelp = showGameHelp;
            _showExitConfirmation = showExitConfirmation;
            _menuViewModel = new MenuViewModel(StartGame, ShowHelp, ConfirmExit);
            _currentView = _menuViewModel;
        }

        public void StartGame()
        {
            _gameViewModel = new GameViewModel(
                () => _showGameOver(true),
                () => _showGameOver(false),
                _showPause,
                _showGameHelp
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
            _showView(new HelpViewModel(ShowMenu));
        }

        public void ConfirmExit()
        {
            _showExitConfirmation();
        }
    }
}
