using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml.Media.Imaging;
using QRCoder;
using QuickCode.Components.Data;
using System;
using System.Diagnostics;
using System.IO;

namespace QuickCode.ViewModel
{
    public partial class QrCodeGeneratorViewModel : ObservableObject
    {
        #region Fields
        [ObservableProperty] public  QrCodeGenerationType codeGenType = QrCodeGenerationType.Text;
        [ObservableProperty] private string? code;
        [ObservableProperty] private int qrCodeWidth = 128;
        [ObservableProperty] private int qrCodeHeight = 128;
        #endregion

        #region Constructors
        public QrCodeGeneratorViewModel()
        {
            // only supported version for now
            CodeGenerationTypeOptions = new QrCodeGenerationType[] { QrCodeGenerationType.Text };
        }
        #endregion

        #region Properties
        public QrCodeGenerationType[] CodeGenerationTypeOptions { get; }
        public WriteableBitmap? QrCodePreview { get; private set; }
        #endregion

        #region Methods
        [RelayCommand] private void UpdateQrCodePreview()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Code))
                {
                    QrCodePreview = null;
                    OnPropertyChanged(nameof(QrCodePreview));
                    return;
                }
                using var qrGenerator = new QRCodeGenerator();
                using var qrCodeData = qrGenerator.CreateQrCode(Code, QRCodeGenerator.ECCLevel.Q);
                using var qrCode = new BitmapByteQRCode(qrCodeData);
                var pixels = qrCode.GetGraphic(20);

                var writeableBitmap = new WriteableBitmap(QrCodeWidth, QrCodeHeight);
                using var stream = new MemoryStream(pixels);
                using var randomAccessStream = stream.AsRandomAccessStream();
                randomAccessStream.Seek(0);
                writeableBitmap.SetSource(randomAccessStream);
                writeableBitmap.Invalidate();
                QrCodePreview = writeableBitmap;
                OnPropertyChanged(nameof(QrCodePreview));
            }
            catch (Exception ex) { Debug.WriteLine(ex); }
        }
        #endregion
    }
}
