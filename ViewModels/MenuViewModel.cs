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
        public RelayCommand HelpCommand { get; }
        public RelayCommand ExitCommand { get; }

        private readonly Action _onStartGame;
        private readonly Action _onShowHelp;

        public MenuViewModel(Action onStartGame, Action onShowHelp)
        {
            _onStartGame = onStartGame;
            _onShowHelp = onShowHelp;
            StartGameCommand = new RelayCommand(StartGame);
            HelpCommand = new RelayCommand(ShowHelp);
            ExitCommand = new RelayCommand(Exit);
        }

        private void StartGame()
        {
            _onStartGame();
        }

        private void ShowHelp()
        {
            _onShowHelp();
        }

        private void Exit()
        {
            Application.Current.Shutdown();
        }
    }
}
