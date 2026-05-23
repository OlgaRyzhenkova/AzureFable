using AzureFable.ViewModels;
using System.Windows;
using System.Windows.Input;

namespace AzureFable
{
    public partial class MainWindow : Window
    {
        private MainViewModel? _mainViewModel;

        public MainWindow()
        {
            InitializeComponent();
            KeyDown += MainWindow_KeyDown;
            _mainViewModel = new MainViewModel(
                ShowGameOverScreen,
                ShowPauseScreen,
                ShowGameHelpScreen,
                ShowExitConfirmation,
                ShowError);
            DataContext = _mainViewModel;
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (_mainViewModel?.CurrentView is GameViewModel gameViewModel)
            {
                gameViewModel.MoveHero(e.Key);
            }
        }

        private void ShowGameOverScreen(bool isWin)
        {
            HideOverlay();
            OverlayContent.Content = new Views.GameOverView(
                isWin,
                () =>
                {
                    HideOverlay();
                    _mainViewModel?.StartGame();
                },
                () =>
                {
                    HideOverlay();
                    _mainViewModel?.ShowMenu();
                }
            );
        }

        private void ShowPauseScreen()
        {
            OverlayContent.Content = new Views.PauseView(
                () =>
                {
                    HideOverlay();
                    _mainViewModel?.ResumeGame();
                },
                () =>
                {
                    HideOverlay();
                    _mainViewModel?.StartGame();
                },
                () =>
                {
                    HideOverlay();
                    _mainViewModel?.ShowMenu();
                }
            );
        }

        private void ShowGameHelpScreen()
        {
            var view = new Views.HelpView();
            view.DataContext = new HelpViewModel(() =>
            {
                HideOverlay();
                _mainViewModel?.ResumeGame();
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

        private void ShowError(string message)
        {
            MessageBox.Show(message, "Помилка запуску", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
