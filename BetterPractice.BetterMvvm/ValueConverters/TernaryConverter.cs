using System;
using System.Collections.Generic;
using System.Globalization;
using Xamarin.Forms;

#nullable disable

namespace BetterPractice.BetterMvvm.ValueConverters
{
    public class TernaryConverter<T> : BindableObject, IValueConverter
    {
        public static readonly BindableProperty TrueValueProperty = BindableProperty.Create(nameof(TrueValue), typeof(T), typeof(TernaryConverter<T>));
        public static readonly BindableProperty FalseValueProperty = BindableProperty.Create(nameof(FalseValue), typeof(T), typeof(TernaryConverter<T>));

        public T TrueValue
        {
            get => (T)GetValue(TrueValueProperty);
            set => SetValue(TrueValueProperty, value);
        }

        public T FalseValue
        {
            get => (T)GetValue(FalseValueProperty);
            set => SetValue(FalseValueProperty, value);
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var boolValue = (bool)value;
            return boolValue ? TrueValue : FalseValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
