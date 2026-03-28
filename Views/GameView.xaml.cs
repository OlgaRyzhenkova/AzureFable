using AzureFable.ViewModels;
using System.Windows;
using System.Windows.Input;

namespace AzureFable.Views
{
    public partial class GameView : Window
    {
        private GameViewModel? _viewModel;

        public GameView()
        {
            InitializeComponent();
        }

        internal void SetViewModel(GameViewModel viewModel)
        {
            _viewModel = viewModel;
            DataContext = _viewModel;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            _viewModel?.MoveHero(e.Key);
        }
    }
}