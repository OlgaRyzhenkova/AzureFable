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
                _mainViewModel = new MainViewModel(ShowView, ShowGameOverScreen);
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
        }

        private void ShowGameOverScreen(bool isWin)
        {
            _gameView = null;
            MainContent.Content = new Views.GameOverView(
                isWin,
                () => _mainViewModel.StartGame(),
                () => _mainViewModel.ShowMenu()
            );
        }
    }
}