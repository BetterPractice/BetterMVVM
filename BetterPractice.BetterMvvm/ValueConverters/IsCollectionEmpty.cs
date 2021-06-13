using System;
using System.Collections;
using System.Linq;
using System.Globalization;

namespace BetterPractice.BetterMvvm.ValueConverters
{
    public class IsCollectionEmpty : ValueConverter<IEnumerable, bool>
    {
        protected override bool Convert(IEnumerable value, object? parameter, CultureInfo culture)
        {
            if (value is IList list)
                return list.Count == 0;

            return !value.GetEnumerator().MoveNext();                
        }

        protected override IEnumerable ConvertBack(bool value, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class IsCollectionNotEmpty : ValueConverter<IEnumerable, bool>
    {
        protected override bool Convert(IEnumerable value, object? parameter, CultureInfo culture)
        {
            if (value is IList list)
                return list.Count > 0;

            return value?.GetEnumerator()?.MoveNext() ?? false;
        }

        protected override IEnumerable ConvertBack(bool value, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
