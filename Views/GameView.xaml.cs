using AzureFable.ViewModels;
using System.Windows.Controls;
using System.Windows.Input;

namespace AzureFable.Views
{
    public partial class GameView : UserControl
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
            Focusable = true;
            Focus();
        }

        private void UserControl_KeyDown(object sender, KeyEventArgs e)
        {
            _viewModel?.MoveHero(e.Key);
        }
    }
}