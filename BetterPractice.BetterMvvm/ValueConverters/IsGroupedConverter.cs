using System;
using System.Collections;
using System.Globalization;
using System.Linq;

namespace BetterPractice.BetterMvvm.ValueConverters
{
    public class IsGroupedConverter : ValueConverter<IList, bool>
    {
        protected override bool Convert(IList value, object? parameter, CultureInfo culture)
        {
            var first = value.Count > 0 ? value[0] : null;
            return first is IList;
        }

        protected override IList ConvertBack(bool value, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
