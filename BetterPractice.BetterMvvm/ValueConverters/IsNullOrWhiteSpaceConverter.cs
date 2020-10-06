using System;
using System.Globalization;

namespace BetterPractice.BetterMvvm.ValueConverters
{
    public class IsNullOrWhiteSpaceConverter : ValueConverter<string?, bool>
    {
        protected override bool Convert(string? value, object? parameter, CultureInfo culture)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        protected override string? ConvertBack(bool value, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class IsNotNullOrWhiteSpaceConverter : ValueConverter<string?, bool>
    {
        protected override bool Convert(string? value, object? parameter, CultureInfo culture)
        {
            return !string.IsNullOrWhiteSpace(value);
        }

        protected override string? ConvertBack(bool value, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
