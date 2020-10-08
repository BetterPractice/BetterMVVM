using System;
using System.Text.RegularExpressions;

namespace BetterPractice.BetterMvvm.ViewModels.TextValidators
{
    public class RegexValidator : ITextValidator
    {
        public Regex Regex { get; }

        public RegexValidator(Regex regex)
        {
            Regex = regex;
        }

        public bool Validate(string? text)
        {
            return Regex.IsMatch(text);
        }
    }
}
