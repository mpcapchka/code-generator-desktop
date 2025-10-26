using Microsoft.UI.Xaml.Data;
using System;

namespace QuickCode.Components.Converters
{
    public class BusyStatusOpacityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is not bool isBusy) return value;
            return isBusy ? 0.5d : 1.0d;
        }
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
