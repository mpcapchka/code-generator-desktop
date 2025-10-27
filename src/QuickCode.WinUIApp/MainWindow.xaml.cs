using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using QuickCode.Pages;

namespace QuickCode
{
    public sealed partial class MainWindow : Window
    {
        #region Fields
        private readonly Microsoft.Windows.ApplicationModel.Resources.ResourceLoader resourceLoader = new();
        #endregion

        #region Constructors
        public MainWindow()
        {
            InitializeComponent();

            this.ExtendsContentIntoTitleBar = true; // Extend the content into the title bar and hide the default titlebar
            this.SetTitleBar(titleBar); // Set the custom title bar
        }
        #endregion

        #region Properties
        public XamlRoot XamlRoot { get => gridXamlRoot.XamlRoot; }
        #endregion

        #region Handlers
        private void titleBar_PaneToggleRequested(TitleBar sender, object args)
        {
            navView.IsPaneOpen = !navView.IsPaneOpen;
        }
        private void navView_Loaded(object sender, RoutedEventArgs e)
        {
            navView.SelectedItem = qrCodeGenerationNavItem;
        }
        private void navView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.SelectedItem is not NavigationViewItem item) return;
            var tagStringVvalue = item.Tag?.ToString();
            switch (tagStringVvalue)
            {
                case "QrCodeGenerationPage": navFrame.Navigate(typeof(QrCodeGeneratorPage)); break;
                case "Settings": navFrame.Navigate(typeof(SettingsPage)); break;
                default: break;
            }
        }
        #endregion
    }
}
