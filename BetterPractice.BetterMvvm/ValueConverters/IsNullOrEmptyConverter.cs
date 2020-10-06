using System;
using System.Globalization;

namespace BetterPractice.BetterMvvm.ValueConverters
{
    public class IsNullOrEmptyConverter : ValueConverter<string?, bool>
    {
        protected override bool Convert(string? value, object? parameter, CultureInfo culture)
        {
            return string.IsNullOrEmpty(value);
        }

        protected override string? ConvertBack(bool value, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class IsNotNullOrEmptyConverter : ValueConverter<string?, bool>
    {
        protected override bool Convert(string? value, object? parameter, CultureInfo culture)
        {
            return !string.IsNullOrEmpty(value);
        }

        protected override string? ConvertBack(bool value, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
