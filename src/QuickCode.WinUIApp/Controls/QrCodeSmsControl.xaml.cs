using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace QuickCode.Controls
{
    public sealed partial class QrCodeSmsControl : UserControl
    {
        public QrCodeSmsControl()
        {
            InitializeComponent();
        }
        private void NumericTextBox_BeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            // The control already does basic filtering based on InputScope="Number"
            // However, you may need additional logic to limit characters like '+', 'e', etc. 

            // Example for strictly allowing only digits and one optional negative sign at the start:
            // This example is for illustration and needs refinement for decimals, etc.
            if (args.NewText.Length > 0 && !Regex.IsMatch(args.NewText, "^-?[0-9]*\\.?[0-9]*$"))
            {
                args.Cancel = true;
            }
        }
    }
}
