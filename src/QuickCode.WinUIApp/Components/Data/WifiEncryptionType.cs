namespace QuickCode.Components.Data
{
    /// <summary>
    /// Defines the supported authentication and encryption modes for Wi-Fi QR codes.
    /// </summary>
    public enum WifiEncryptionType
    {
        /// <summary>
        /// No password is required for the Wi-Fi network.
        /// </summary>
        None,
        /// <summary>
        /// WEP protected network.
        /// </summary>
        WEP,
        /// <summary>
        /// WPA/WPA2 protected network.
        /// </summary>
        WPA,
        /// <summary>
        /// WPA2 protected network (explicit).
        /// </summary>
        WPA2
    }
}
