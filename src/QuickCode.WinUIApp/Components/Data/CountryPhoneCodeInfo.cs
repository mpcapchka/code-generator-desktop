using System.Globalization;

namespace QuickCode.Components.Data
{
    public class CountryPhoneCodeInfo
    {
        #region Properties
        /// <summary>
        /// ISO 3166-1 Alpha-2 code (e.g., "AW")
        /// </summary>
        public string RegionCode { get; init; } = string.Empty;

        /// <summary>
        /// Numeric code (e.g., 297)
        /// </summary>
        public int CountryCallingCode { get; init; } // 

        /// <summary>
        /// Localized name (e.g., "Aruba")
        /// </summary>
        public string EnglishName { get; init; } = string.Empty;

        public string DisplayInfo { get => $"+{CountryCallingCode} {EnglishName}"; }
        #endregion
    }
}
