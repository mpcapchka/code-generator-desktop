using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using QuickCode.Controls;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Storage;

namespace QuickCode.ViewModels
{
    public partial class SettingsPageViewModel : ObservableObject
    {
        #region Fields
        private CultureInfo selCulture = new("en-US");
        #endregion

        #region Constructors
        public SettingsPageViewModel()
        {
            BuildVersion = Assembly.GetExecutingAssembly().GetName().Version?.ToString()!;
            PublisherName = Package.Current.PublisherDisplayName;
            ApplicationName = Package.Current.DisplayName;
            UpdateBuildYearSafely();

            AvailableCultures = new CultureInfo[] { selCulture };
        }
        #endregion

        #region Properties
        public CultureInfo SelectedCulture { get => selCulture; set { selCulture = value; OnPropertyChanged(); } }
        public CultureInfo[] AvailableCultures { get; }
        public int BuildYear { get; private set; } = 0;
        public string BuildVersion { get; } = string.Empty;
        public string PublisherName { get; }
        public string ApplicationName { get; }
        #endregion

        #region Methods
        [RelayCommand] private async Task ShowThirdPartySoftwareAcknowledgments()
        {
            try
            {
                var xamlRoot = App.Current.MainWindow?.XamlRoot;
                ArgumentNullException.ThrowIfNull(xamlRoot);

                var dialog = new ContentDialog();
                dialog.XamlRoot = xamlRoot; 
                dialog.Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style;
                dialog.CloseButtonText = "Close";
                dialog.DefaultButton = ContentDialogButton.Close;
                var rawText = await GetDependenciesText();
                dialog.Content = new MarkdownTextControl(rawText);
                var result = await dialog.ShowAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
        #endregion

        #region Helpers
        private async Task<string> GetDependenciesText()
        {
            var file = await StorageFile.GetFileFromApplicationUriAsync(
                    new Uri("ms-appx:///DEPENDENCIES.md"));
            return await FileIO.ReadTextAsync(file);
        }
        private async Task<DateTime> GetBuildDate()
        {
            var file = await StorageFile.GetFileFromApplicationUriAsync(
                new Uri("ms-appx:///BuildDate.txt"));
            string dateString = await FileIO.ReadTextAsync(file);
            return DateTime.Parse(dateString);
        }
        private async void UpdateBuildYearSafely()
        {
            try
            {
                var date = await GetBuildDate();
                BuildYear = date.Year;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            OnPropertyChanged(nameof(BuildYear));
        }
        #endregion
    }
}
