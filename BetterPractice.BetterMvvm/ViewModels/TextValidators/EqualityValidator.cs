using System;
namespace BetterPractice.BetterMvvm.ViewModels.TextValidators
{
    public class EqualityValidator : ITextValidator
    {
        public Func<string?> DynamicAccessor { get; }

        public string? ExpectedValue => DynamicAccessor();

        public EqualityValidator(string? value)
        {
            DynamicAccessor = () => value;
        }

        public EqualityValidator(Func<string?> dynamicAccessor)
        {
            DynamicAccessor = dynamicAccessor;
        }

        public bool Validate(string? text)
        {
            return text == ExpectedValue;
        }
    }
}
