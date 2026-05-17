using System;

namespace AzureFable.ViewModels
{
    internal class HelpViewModel : ViewModelBase
    {
        public RelayCommand BackCommand { get; }

        public HelpViewModel(Action onBack)
        {
            BackCommand = new RelayCommand(onBack);
        }
    }
}
