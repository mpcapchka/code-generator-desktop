using System;

namespace QuickCode.ViewModels
{
    /// <summary>
    /// Represents a contract for ViewModels that manage the specific data 
    /// required to generate a particular type of QR code (e.g., Text, URL, Email).
    /// </summary>
    public interface IQrCodeDataViewModel
    {
        #region Event

        /// <summary>
        /// Occurs when the raw, finalized data string for QR code generation is ready or updated.
        /// The event argument (string?) should contain the fully formatted data payload 
        /// (e.g., a 'tel:' URI, 'mailto:' URI, or plain text).
        /// </summary>
        public event EventHandler<string?>? RawDataReceived;
        #endregion
    }
}
