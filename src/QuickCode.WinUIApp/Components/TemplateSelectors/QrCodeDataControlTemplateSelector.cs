using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using QuickCode.ViewModels;
using System;

namespace QuickCode.Components.TemplateSelectors
{
    public class QrCodeDataControlTemplateSelector : DataTemplateSelector
    {
        #region Properties
        public DataTemplate TextDataControlTemplate { get; set; } = null!;
        public DataTemplate CallDataControlTemplate { get; set; } = null!;
        public DataTemplate SmsDataControlTemplate { get; set; } = null!;
        #endregion

        #region Methods
        protected override DataTemplate SelectTemplateCore(object item)
        {
            if (item == null) return null!;

            return item switch
            {
                QrCodeTextDataViewModel => TextDataControlTemplate,
                QrCodeCallDataViewModel => CallDataControlTemplate,
                QrCodeSmsDataViewModel => SmsDataControlTemplate,
                _ => throw new NotImplementedException($"Not supported type: \"{item.GetType().Name}\"")
            };
        }
        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            return SelectTemplateCore(item);
        }
        #endregion
    }
}
