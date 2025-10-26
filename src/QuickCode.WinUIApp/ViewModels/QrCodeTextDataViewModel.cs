using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace QuickCode.ViewModels
{
    public class QrCodeTextDataViewModel : ObservableObject, IQrCodeDataViewModel
    {
        #region Fields
        private string? text;
        #endregion

        #region Events
        public event EventHandler<string?>? RawDataReceived;
        #endregion

        #region Properties
        public string? Text { get => text; set { text = value; OnTextChanged(value); } }
        #endregion

        #region Handlers
        private void OnTextChanged(string? value)
        {
            RawDataReceived?.Invoke(this, value);
            OnPropertyChanged(nameof(Text));
        }
        #endregion
    }
}
