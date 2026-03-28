using AzureFable.ViewModels;
using System.Windows;

namespace AzureFable
{
    public partial class MainWindow : Window
    {
        private readonly MainViewModel _mainViewModel;

        public MainWindow()
        {
            InitializeComponent();
            try
            {
                _mainViewModel = new MainViewModel(ShowView);
                ShowView(_mainViewModel.CurrentView);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.StackTrace, "Помилка");
            }
        }

        private void ShowView(ViewModelBase viewModel)
        {
            if (viewModel is MenuViewModel menuViewModel)
            {
                var view = new Views.MenuView();
                view.DataContext = menuViewModel;
                MainContent.Content = view;
            }
            else if (viewModel is GameViewModel gameViewModel)
            {
                var view = new Views.GameView();
                view.SetViewModel(gameViewModel);
                MainContent.Content = view;
            }
        }
    }
}