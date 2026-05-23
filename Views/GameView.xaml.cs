using AzureFable.ViewModels;
using System.Windows.Controls;
using System.Windows.Input;

namespace AzureFable.Views
{
    public partial class GameView : UserControl
    {
        private GameViewModel? ViewModel => DataContext as GameViewModel;

        public GameView()
        {
            InitializeComponent();
        }

        private void UserControl_KeyDown(object sender, KeyEventArgs e)
        {
            ViewModel?.MoveHero(e.Key);
        }
    }
}
