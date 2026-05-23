using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Effects;

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
            ApplyResult(isWin);
        }

        private void ApplyResult(bool isWin)
        {
            if (isWin)
            {
                TitleText.Text = "ПЕРЕМОГА!";
                TitleText.Foreground = new SolidColorBrush(Color.FromRgb(245, 200, 66));
                SubtitleText.Text = "Ви знайшли ключ і вийшли з лабіринту!";
                SetTitleGlow(Color.FromRgb(245, 200, 66));
                return;
            }

            TitleText.Text = "ГРУ ЗАВЕРШЕНО";
            TitleText.Foreground = new SolidColorBrush(Color.FromRgb(220, 48, 58));
            SubtitleText.Text = "Усі життя вичерпано. Спробуйте ще раз!";
            SetTitleGlow(Color.FromRgb(220, 48, 58));
        }

        private void SetTitleGlow(Color color)
        {
            if (TitleText.Effect is DropShadowEffect glow)
            {
                glow.Color = color;
            }
        }

        private void PlayAgainButton_Click(object sender, RoutedEventArgs e)
        {
            _onPlayAgain();
        }

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            _onMenu();
        }
    }
}
