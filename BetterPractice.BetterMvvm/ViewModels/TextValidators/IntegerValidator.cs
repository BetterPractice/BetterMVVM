using System;
namespace BetterPractice.BetterMvvm.ViewModels.TextValidators
{
    public class IntegerValidator : ITextValidator
    {
        public int MinValue { get; }
        public int MaxValue { get; }

        public IntegerValidator(int min = 0, int max = int.MaxValue)
        {
            MinValue = min;
            MaxValue = max;
        }

        public int? ParsedValue { get; private set; }

        public bool Validate(string? text)
        {
            if (text == null)
                return false;

            if (!int.TryParse(text, out var parsed))
            {
                ParsedValue = null;
                return false;
            }
            ParsedValue = parsed;

            return parsed >= MinValue && parsed <= MaxValue;
        }
    }
}
