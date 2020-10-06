using System;
using System.Globalization;

namespace BetterPractice.BetterMvvm.ValueConverters
{
    public class LogicalInverseConverter : ValueConverter<bool, bool>
    {
        protected override bool Convert(bool value, object? parameter, CultureInfo culture)
        {
            return !value;
        }

        protected override bool ConvertBack(bool value, object? parameter, CultureInfo culture)
        {
            return !value;
        }
    }
}
