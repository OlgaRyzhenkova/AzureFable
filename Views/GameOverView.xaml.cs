using System;
using System.Windows.Controls;

namespace AzureFable.Views
{
    public partial class GameOverView : UserControl
    {
        private readonly Action _onPlayAgain;
        private readonly Action _onMenu;

        public GameOverView(bool isWin, Action onPlayAgain, Action onMenu)
        {
            InitializeComponent();

            _onPlayAgain = onPlayAgain;
            _onMenu = onMenu;

            if (isWin)
            {
                TitleText.Text = "Перемога!";
                TitleText.Foreground = new System.Windows.Media.SolidColorBrush(
                    System.Windows.Media.Color.FromRgb(91, 200, 245));
                SubtitleText.Text = "Ви пройшли лабіринт!";
            }
            else
            {
                TitleText.Text = "Кінець гри";
                TitleText.Foreground = new System.Windows.Media.SolidColorBrush(
                    System.Windows.Media.Color.FromRgb(220, 50, 50));
                SubtitleText.Text = "Ви загинули у лабіринті...";
            }
        }

        private void PlayAgainButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            _onPlayAgain();
        }

        private void MenuButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            _onMenu();
        }
    }
}