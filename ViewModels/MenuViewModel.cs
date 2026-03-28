using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureFable.ViewModels
{
    internal class MenuViewModel : ViewModelBase
    {
        public RelayCommand StartGameCommand { get; }
        public RelayCommand ExitCommand { get; }

        private readonly Action _onStartGame;

        public MenuViewModel(Action onStartGame)
        {
            _onStartGame = onStartGame;
            StartGameCommand = new RelayCommand(StartGame);
            ExitCommand = new RelayCommand(Exit);
        }

        private void StartGame()
        {
            _onStartGame();
        }

        private void Exit()
        {
            Application.Current.Shutdown();
        }
    }
}
