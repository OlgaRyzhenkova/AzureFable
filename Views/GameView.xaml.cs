using AzureFable.ViewModels;
using System.Windows.Controls;
using System.Windows.Input;

namespace AzureFable.Views
{
    public partial class GameView : UserControl
    {
        internal GameViewModel? ViewModel { get; private set; }

        public GameView()
        {
            InitializeComponent();
        }

        internal void SetViewModel(GameViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = viewModel;
        }

        private void UserControl_KeyDown(object sender, KeyEventArgs e)
        {
            ViewModel?.MoveHero(e.Key);
        }
    }
}