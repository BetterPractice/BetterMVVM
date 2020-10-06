using System;

namespace BetterPractice.BetterMvvm.ViewModels.TextValidators
{
    public class LengthValidator : ITextValidator
    {
        public int MinimumLength { get; }
        public int MaximumLength { get; }

        public LengthValidator(int min = 0, int max = int.MaxValue)
        {
            MinimumLength = min;
            MaximumLength = max;
        }

        public virtual string GetMinLengthError(int actualLength) => $"Please enter at least {MinimumLength} characters.";
        public virtual string GetMaxLengthError(int actualLength) => $"Please enter less than {MaximumLength} characters.";

        public string? Validate(string? text)
        {
            var length = text?.Length ?? 0;
            if (length < MinimumLength)
                return GetMinLengthError(length);
            if (length > MaximumLength)
                return GetMaxLengthError(length);
            return null;
        }
    }
}
