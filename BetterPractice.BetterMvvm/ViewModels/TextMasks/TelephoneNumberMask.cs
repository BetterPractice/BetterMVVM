using System;
using System.Text;
using BetterPractice.BetterMvvm.Extensions;

namespace BetterPractice.BetterMvvm.ViewModels.TextMasks
{
    public class TelephoneNumberMask : ITextMask
    {
        public string? Transform(string? newValue, string? oldValue)
        {
            if (newValue == null)
                return null;

            var digits = newValue!.MaskToDigits();
            var length = digits.Length;

            var builder = new StringBuilder();

            if (length > 0)
            {
                builder.Append("(");
                builder.Append(digits.Substring(0, Math.Min(length, 3)));
            }
            if (length > 3)
            {
                builder.Append(") ");
                builder.Append(digits.Substring(3, Math.Min(length, 6) - 3));
            }
            if (length > 6)
            {
                builder.Append("-");
                builder.Append(digits.Substring(6, Math.Min(length, 10) - 6));
            }

            return builder.ToString();            
        }
    }
}
