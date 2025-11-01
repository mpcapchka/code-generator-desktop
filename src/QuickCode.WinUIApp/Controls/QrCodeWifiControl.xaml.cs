using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using QuickCode.ViewModels;

namespace QuickCode.Controls
{
    public sealed partial class QrCodeWifiControl : UserControl
    {
        public QrCodeWifiControl()
        {
            InitializeComponent();
        }

        private void OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is QrCodeWifiDataViewModel viewModel)
            {
                viewModel.Password = PasswordBox.Password;
            }
        }

        private void OnDataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            if (args.NewValue is QrCodeWifiDataViewModel viewModel)
            {
                PasswordBox.Password = viewModel.Password;
            }
            else
            {
                PasswordBox.Password = string.Empty;
            }
        }
    }
}
