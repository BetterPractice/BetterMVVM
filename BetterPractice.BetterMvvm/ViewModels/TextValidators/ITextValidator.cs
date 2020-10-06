using System;

namespace BetterPractice.BetterMvvm.ViewModels.TextValidators
{
    public interface ITextValidator
    {
        string? Validate(string? text);
    }

}
