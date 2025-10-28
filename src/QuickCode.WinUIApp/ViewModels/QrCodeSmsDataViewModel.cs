using CommunityToolkit.Mvvm.ComponentModel;
using PhoneNumbers;
using QuickCode.Components.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;

namespace QuickCode.ViewModels
{
    public class QrCodeSmsDataViewModel : ObservableObject, IQrCodeDataViewModel
    {
        #region Fields
        private CountryPhoneCodeInfo selPhoneCode = null!;
        private string phoneNumber = string.Empty;
        private string smsText = string.Empty;
        #endregion

        #region Constructors
        public QrCodeSmsDataViewModel()
        {
            PhoneCodeInfos = GetPhoneCodeToRegionCodeMap();
            SelectedPhoneCode = PhoneCodeInfos.First();
        }
        #endregion

        #region Events
        public event EventHandler<string?>? RawDataReceived;
        #endregion

        #region Properties
        public CountryPhoneCodeInfo[] PhoneCodeInfos { get; }
        public CountryPhoneCodeInfo SelectedPhoneCode { get => selPhoneCode; set { selPhoneCode = value; OnSelectedCodeChanged(); } }
        public string PhoneNumber { get => phoneNumber; set { phoneNumber = value; OnPhoneNumberChanged(); } }
        public string SmsText { get => smsText; set { smsText = value; OnSmsTextChanged(); } }
        #endregion

        #region Handlers
        private void OnSelectedCodeChanged()
        {
            SendRawData();
            OnPropertyChanged(nameof(SelectedPhoneCode));
        }
        private void OnPhoneNumberChanged()
        {
            SendRawData();
            OnPropertyChanged(nameof(PhoneNumber));
        }
        private void OnSmsTextChanged()
        {
            SendRawData();
            OnPropertyChanged(nameof(SmsText));
        }
        #endregion

        #region Helpers
        private void SendRawData()
        {
            if (SelectedPhoneCode == null || string.IsNullOrWhiteSpace(PhoneNumber) || string.IsNullOrWhiteSpace(SmsText))
            {
                RawDataReceived?.Invoke(this, null);
                return;
            }
            RawDataReceived?.Invoke(this, $"smsto:+{SelectedPhoneCode.CountryCallingCode}{phoneNumber}:{SmsText}");
        }
        private CountryPhoneCodeInfo[] GetPhoneCodeToRegionCodeMap()
        {
            var phoneUtil = PhoneNumberUtil.GetInstance();
            var allRegionCodes = phoneUtil.GetSupportedRegions();
            var resultList = new List<CountryPhoneCodeInfo>();
            var restrictedRegions = new string[] { "TA", "EH", "AC", "RU" };

            foreach (var regionCode in allRegionCodes)
            {
                try
                {
                    if (restrictedRegions.Contains(regionCode)) continue;
                    // 1. Get the numeric country calling code (e.g., 297)
                    int countryCode = phoneUtil.GetCountryCodeForRegion(regionCode);

                    // 2. Get the localized region name (using RegionInfo and a default culture like English)
                    //    We use the RegionInfo created from the ISO code ("AW")
                    var regionInfo = new RegionInfo(regionCode);
                    string englishName = regionInfo.EnglishName;

                    resultList.Add(new CountryPhoneCodeInfo
                    {
                        RegionCode = regionCode,
                        CountryCallingCode = countryCode,
                        EnglishName = englishName
                    });
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error processing region {regionCode}: {ex.Message}");
                }
            }
            return resultList.OrderBy(x => x.EnglishName).ToArray();
        }
        #endregion
    }
}
