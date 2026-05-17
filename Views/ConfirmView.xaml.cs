using System;
using System.Windows;
using System.Windows.Controls;

namespace AzureFable.Views
{
    public partial class ConfirmView : UserControl
    {
        private readonly Action _onConfirm;
        private readonly Action _onCancel;

        public ConfirmView(string question, Action onConfirm, Action onCancel)
        {
            InitializeComponent();

            QuestionText.Text = question;
            _onConfirm = onConfirm;
            _onCancel = onCancel;
        }

        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
            _onConfirm();
        }

        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            _onCancel();
        }
    }
}
