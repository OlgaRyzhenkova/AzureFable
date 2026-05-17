using System;
using System.Windows;
using System.Windows.Controls;

namespace AzureFable.Views
{
    public partial class PauseView : UserControl
    {
        private readonly Action _onResume;
        private readonly Action _onRestart;
        private readonly Action _onMenu;

        public PauseView(Action onResume, Action onRestart, Action onMenu)
        {
            InitializeComponent();

            _onResume = onResume;
            _onRestart = onRestart;
            _onMenu = onMenu;
        }

        private void ResumeButton_Click(object sender, RoutedEventArgs e)
        {
            _onResume();
        }

        private void RestartButton_Click(object sender, RoutedEventArgs e)
        {
            _onRestart();
        }

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            _onMenu();
        }
    }
}
