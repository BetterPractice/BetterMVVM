using System;
using System.Collections.Generic;
using Xamarin.Forms;
using BetterPractice.BetterMvvm.ViewModels.TextMasks;
using BetterPractice.BetterMvvm.ViewModels.TextValidators;
using System.Linq;

namespace BetterPractice.BetterMvvm.ViewModels
{
    public class TextFieldViewModel : ObservableObject, IValidated
    {
        public class ValidationCompleteArgs
        {
            public string? OldValue { get; set; }
            public string? NewValue { get; set; }
            public IEnumerable<ITextValidator>? FailedValidators { get; set; }
        }

        public delegate void ValidationCompleteHandler(TextFieldViewModel sender, ValidationCompleteArgs args);

        private bool _isValid;
        public bool IsValid
        {
            get => _isValid;
            set => SetProperty(ref _isValid, value);
        }

        private string? _validationErrorMessage;
        public string? ValidationErrorMessage
        {
            get => _validationErrorMessage;
            set => SetProperty(ref _validationErrorMessage, value);
        }

        private Keyboard _keyboard = Keyboard.Default;
        public Keyboard Keyboard
        {
            get => _keyboard;
            set => SetProperty(ref _keyboard, value);
        }

        private string? _text;
        public string? Text
        {
            get => _text;
            set
            {
                var oldValue = _text;
                if (SetProperty(ref _text, value))
                    OnTextChanged(oldValue);
            }
        }

        private string? _placeholder;
        public string? PlaceHolder
        {
            get => _placeholder;
            set => SetProperty(ref _placeholder, value);
        }

        private string? _label;
        public string? Label
        {
            get => _label;
            set => SetProperty(ref _label, value);
        }

        private string? _description;
        public string? Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        private string? _footnote;
        public string? Footnote
        {
            get => _footnote;
            set => SetProperty(ref _footnote, value);
        }

        private List<ITextMask> _textMasks = new List<ITextMask>();
        public List<ITextMask> TextMasks
        {
            get => _textMasks;
            set => SetProperty(ref _textMasks, value);
        }

        private List<ITextValidator> _validators = new List<ITextValidator>();
        public List<ITextValidator> Validators
        {
            get => _validators;
            set => SetProperty(ref _validators, value);
        }

        public event ValidationCompleteHandler? ValidationComplete;

        protected virtual void OnTextChanged(string? oldValue)
        {
            if (oldValue == Text)
                return;

            var transformed = Text;
            foreach (var mask in TextMasks)
            {
                transformed = mask.Transform(transformed, oldValue);
            }

            if (transformed != Text)
            {
                Text = transformed;
            }

            var failedValidators = Validators.Where(v => !v.Validate(Text));
            IsValid = failedValidators.Any();
            var args = new ValidationCompleteArgs
            {
                OldValue = oldValue,
                NewValue = Text,
                FailedValidators = failedValidators,
            };
            ValidationComplete?.Invoke(this, args);
        }
    }
}