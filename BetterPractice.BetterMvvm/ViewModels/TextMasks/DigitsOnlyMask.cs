using System;
using BetterPractice.BetterMvvm.Extensions;

namespace BetterPractice.BetterMvvm.ViewModels.TextMasks
{
    public class DigitsOnlyMask : ITextMask
    {
        public string? Transform(string? newValue, string? oldValue)
        {
            return newValue?.MaskToDigits();
        }
    }
}
