using Microsoft.UI.Xaml.Data;
using Microsoft.Windows.ApplicationModel.Resources;
using QuickCode.Components.Data;
using System;
using Windows.ApplicationModel.Resources;

namespace QuickCode.Components.Converters
{
    public class EnumToLocalizedTextConverter : IValueConverter
    {
        #region Fields
        private readonly Microsoft.Windows.ApplicationModel.Resources.ResourceLoader resourceLoader = new();
        #endregion

        #region Methods
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is QrCodeGenerationType codeGenerationType) return GetLocalizedText(codeGenerationType);
            else return value;
        }
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }

        private string GetLocalizedText(QrCodeGenerationType value)
        {
            switch (value)
            {
                case QrCodeGenerationType.Link: return resourceLoader.GetString("QrCodeGenerationType_Link");
                case QrCodeGenerationType.Text: return resourceLoader.GetString("QrCodeGenerationType_Text"); 
                case QrCodeGenerationType.Email: return resourceLoader.GetString("QrCodeGenerationType_Email"); 
                case QrCodeGenerationType.Call: return resourceLoader.GetString("QrCodeGenerationType_Call"); 
                case QrCodeGenerationType.Sms: return resourceLoader.GetString("QrCodeGenerationType_Sms"); 
                case QrCodeGenerationType.VCard: return resourceLoader.GetString("QrCodeGenerationType_VCard"); 
                case QrCodeGenerationType.WhatsApp: return resourceLoader.GetString("QrCodeGenerationType_WhatsApp"); 
                case QrCodeGenerationType.Wifi: return resourceLoader.GetString("QrCodeGenerationType_Wifi"); 
                case QrCodeGenerationType.Pdf: return resourceLoader.GetString("QrCodeGenerationType_Pdf"); 
                case QrCodeGenerationType.App: return resourceLoader.GetString("QrCodeGenerationType_App"); 
                case QrCodeGenerationType.Image: return resourceLoader.GetString("QrCodeGenerationType_Image"); 
                case QrCodeGenerationType.Video: return resourceLoader.GetString("QrCodeGenerationType_Video"); 
                case QrCodeGenerationType.SocialMedia: return resourceLoader.GetString("QrCodeGenerationType_SocialMedia"); 
                case QrCodeGenerationType.Event: return resourceLoader.GetString("QrCodeGenerationType_Event");
                default: throw new NotImplementedException($"No implementation for such value: {value}");
            }
        }
        #endregion
    }
}
