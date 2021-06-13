using System;
using System.Linq;
using BetterPractice.BetterMvvm.Extensions;

namespace BetterPractice.BetterMvvm.ViewModels.TextMasks
{
    public class LengthMask : ITextMask
    {
        public int MaximumLength { get; }

        public LengthMask(int max)
        {
            MaximumLength = max;
        }

        public string? Transform(string? newValue, string? oldValue)
        {
            return newValue?.Take(MaximumLength).Implode();
        }
    }
}
