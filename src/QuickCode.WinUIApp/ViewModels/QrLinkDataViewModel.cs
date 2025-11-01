using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace QuickCode.ViewModels
{
    public class QrLinkDataViewModel : ObservableObject, IQrCodeDataViewModel
    {
        #region Fields
        private string link = string.Empty;
        #endregion

        #region Events
        public event EventHandler<string?>? RawDataReceived;
        #endregion

        #region Properties
        public string Link
        {
            get => link;
            set
            {
                link = value;
                OnLinkChanged(value);
            }
        }
        #endregion

        #region Handlers
        private void OnLinkChanged(string? value)
        {
            var normalizedLink = NormalizeLink(value);
            RawDataReceived?.Invoke(this, normalizedLink);
            OnPropertyChanged(nameof(Link));
        }
        #endregion

        #region Helpers
        private static string? NormalizeLink(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return null;
            }

            var trimmedValue = value.Trim();

            if (Uri.TryCreate(trimmedValue, UriKind.Absolute, out var absoluteUri))
            {
                return absoluteUri.AbsoluteUri;
            }

            if (Uri.TryCreate($"https://{trimmedValue}", UriKind.Absolute, out var httpsUri))
            {
                return httpsUri.AbsoluteUri;
            }

            return null;
        }
        #endregion
    }
}
