using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureFable.ViewModels
{
    internal class MenuViewModel : ViewModelBase
    {
        public RelayCommand StartGameCommand { get; }
        public RelayCommand SettingsCommand { get; }
        public RelayCommand HelpCommand { get; }
        public RelayCommand ExitCommand { get; }

        private readonly Action _onStartGame;
        private readonly Action _onShowSettings;
        private readonly Action _onShowHelp;
        private readonly Action _onConfirmExit;

        public MenuViewModel(Action onStartGame, Action onShowSettings, Action onShowHelp, Action onConfirmExit)
        {
            _onStartGame = onStartGame;
            _onShowSettings = onShowSettings;
            _onShowHelp = onShowHelp;
            _onConfirmExit = onConfirmExit;
            StartGameCommand = new RelayCommand(StartGame);
            SettingsCommand = new RelayCommand(ShowSettings);
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

        private void ShowSettings()
        {
            _onShowSettings();
        }

        private void Exit()
        {
            _onConfirmExit();
        }
    }
}
