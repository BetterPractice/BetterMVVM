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

        public int ActualLength { get; private set; }

        public bool Validate(string? text)
        {
            var length = text?.Length ?? 0;
            ActualLength = length;
            return length >= MinimumLength && length <= MaximumLength;
        }
    }
}
