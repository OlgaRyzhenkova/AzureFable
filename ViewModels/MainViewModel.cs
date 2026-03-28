using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureFable.ViewModels
{
    internal class MainViewModel : ViewModelBase
    {
        private readonly Action<ViewModelBase> _showView;
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

        public MainViewModel(Action<ViewModelBase> showView)
        {
            _showView = showView;
            _menuViewModel = new MenuViewModel(StartGame);
            _currentView = _menuViewModel;
        }

        private void StartGame()
        {
            _gameViewModel = new GameViewModel();
            CurrentView = _gameViewModel;
        }
    }
}
