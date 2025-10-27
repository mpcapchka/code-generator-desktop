using System;
using System.Globalization;
using System.Linq;

namespace QuickCode.ViewModels
{
    public class QrCodeCallDataViewModel : IQrCodeDataViewModel
    {
        #region Constructors
        public QrCodeCallDataViewModel()
        {
            var countryCodes = CultureInfo.GetCultures(CultureTypes.SpecificCultures)
                .Select(culture => new RegionInfo(culture.Name))
                .GroupBy(region => region.Name)
                .Select(g => g.First())
                .Select(region => new
                {
                    CountryName = region.EnglishName,
                    TwoLetterCode = region.TwoLetterISORegionName,
                    ThreeLetterCode = region.ThreeLetterISORegionName,
                    // Note: RegionInfo doesn't have phone codes directly
                })
                .OrderBy(x => x.CountryName)
                .ToList();
        }
        #endregion

        #region Events
        public event EventHandler<string?>? RawDataReceived;
        #endregion

        #region Properties

        #endregion
    }
}
