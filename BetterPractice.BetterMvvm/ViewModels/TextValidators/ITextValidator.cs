using System;

namespace BetterPractice.BetterMvvm.ViewModels.TextValidators
{
    public interface ITextValidator
    {
        bool Validate(string? text);
    }

}
