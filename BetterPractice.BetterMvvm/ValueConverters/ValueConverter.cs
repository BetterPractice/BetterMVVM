using System;
using System.Globalization;
using Xamarin.Forms;

#nullable disable

namespace BetterPractice.BetterMvvm.ValueConverters
{
    public abstract class ValueConverter<TSource, TTarget> : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var typedValue = (TSource)value;
            return Convert(typedValue, parameter, culture);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var typedValue = (TTarget)value;
            return ConvertBack(typedValue, parameter, culture);
        }


#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        protected abstract TTarget Convert(TSource value, object? parameter, CultureInfo culture);
        protected abstract TSource ConvertBack(TTarget value, object? parameter, CultureInfo culture);
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
    }
}
