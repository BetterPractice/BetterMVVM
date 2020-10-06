using System;
using System.Globalization;

namespace BetterPractice.BetterMvvm.ValueConverters
{
    public class IsNullConverter : ValueConverter<object?, bool>
    {
        protected override bool Convert(object? value, object? parameter, CultureInfo culture)
        {
            return value == null;
        }

        protected override object? ConvertBack(bool value, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class IsNotNullConverter : ValueConverter<object?, bool>
    {
        protected override bool Convert(object? value, object? parameter, CultureInfo culture)
        {
            return value != null;
        }

        protected override object? ConvertBack(bool value, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
