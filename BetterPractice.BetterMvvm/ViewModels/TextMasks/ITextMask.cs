using System;

namespace BetterPractice.BetterMvvm.ViewModels.TextMasks
{
    public interface ITextMask
    {
        string? Transform(string? newValue, string? oldValue);
    }
}
