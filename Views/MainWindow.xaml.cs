using AzureFable.ViewModels;
using System.Windows;
using System.Windows.Input;

namespace AzureFable
{
    public partial class MainWindow : Window
    {
        private readonly MainViewModel _mainViewModel;
        private Views.GameView? _gameView;

        public MainWindow()
        {
            InitializeComponent();
            KeyDown += MainWindow_KeyDown;
            try
            {
                _mainViewModel = new MainViewModel(ShowView, ShowGameOverScreen, ShowPauseScreen, ShowGameHelpScreen, ShowExitConfirmation);
                ShowView(_mainViewModel.CurrentView);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.StackTrace, "Помилка");
            }
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            _gameView?.ViewModel?.MoveHero(e.Key);
        }

        private void ShowView(ViewModelBase viewModel)
        {
            HideOverlay();

            if (viewModel is MenuViewModel menuViewModel)
            {
                var view = new Views.MenuView();
                view.DataContext = menuViewModel;
                MainContent.Content = view;
                _gameView = null;
            }
            else if (viewModel is GameViewModel gameViewModel)
            {
                _gameView = new Views.GameView();
                _gameView.SetViewModel(gameViewModel);
                MainContent.Content = _gameView;
            }
            else if (viewModel is HelpViewModel helpViewModel)
            {
                var view = new Views.HelpView();
                view.DataContext = helpViewModel;
                MainContent.Content = view;
                _gameView = null;
            }
            else if (viewModel is SettingsViewModel settingsViewModel)
            {
                var view = new Views.SettingsView();
                view.DataContext = settingsViewModel;
                MainContent.Content = view;
                _gameView = null;
            }
        }

        private void ShowGameOverScreen(bool isWin)
        {
            HideOverlay();
            _gameView = null;
            MainContent.Content = new Views.GameOverView(
                isWin,
                () => _mainViewModel.StartGame(),
                () => _mainViewModel.ShowMenu()
            );
        }

        private void ShowPauseScreen()
        {
            OverlayContent.Content = new Views.PauseView(
                () =>
                {
                    HideOverlay();
                    _mainViewModel.ResumeGame();
                },
                () =>
                {
                    HideOverlay();
                    _mainViewModel.StartGame();
                },
                () =>
                {
                    HideOverlay();
                    _mainViewModel.ShowMenu();
                }
            );
        }

        private void ShowGameHelpScreen()
        {
            var view = new Views.HelpView();
            view.DataContext = new HelpViewModel(() =>
            {
                HideOverlay();
                _mainViewModel.ResumeGame();
            });
            OverlayContent.Content = view;
        }

        private void ShowExitConfirmation()
        {
            OverlayContent.Content = new Views.ConfirmView(
                "Ви дійсно хочете вийти з гри?",
                () => Application.Current.Shutdown(),
                HideOverlay
            );
        }

        private void HideOverlay()
        {
            OverlayContent.Content = null;
        }
    }
}
