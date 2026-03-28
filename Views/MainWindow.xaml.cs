using AzureFable.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace AzureFable
{
    public partial class MainWindow : Window
    {
        private readonly MainViewModel _mainViewModel;

        public MainWindow()
        {
            InitializeComponent();
            _mainViewModel = new MainViewModel(ShowView);
            ShowView(_mainViewModel.CurrentView);
        }

        private void ShowView(ViewModelBase viewModel)
        {
            if (viewModel is MenuViewModel)
            {
                MainContent.Content = new Views.MenuView();
                MainContent.Content = new Frame();
            }
            else if (viewModel is GameViewModel)
            {
                //MainContent.Content = new Views.GameView();
            }
        }
    }
}