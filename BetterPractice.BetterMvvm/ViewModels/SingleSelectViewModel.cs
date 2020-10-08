using System;
using System.Collections.Generic;

namespace BetterPractice.BetterMvvm.ViewModels
{
    public class SingleSelectViewModel<T> : ObservableObject, IValidated where T : class
    {
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

        public SingleSelectViewModel()
        {
            _choices = new List<T>();
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

        private List<T> _choices;
        public List<T> Choices
        {
            get => _choices;
            set => SetProperty(ref _choices, value);
        }

        private T? _selected;
        public T? Selected
        {
            get => _selected;
            set
            {
                var oldValue = _selected;
                if (SetProperty(ref _selected, value))
                    OnSelectionChanged(oldValue);
            }
        }

        private string? _footnote;
        public string? Footnote
        {
            get => _footnote;
            set => SetProperty(ref _footnote, value);
        }

        private bool _isNullInvalid = true;
        public bool IsNullInvalid
        {
            get => _isNullInvalid;
            set => SetProperty(ref _isNullInvalid, value);
        }

        protected virtual void OnSelectionChanged(T? oldValue)
        {
            IsValid = !IsNullInvalid || Selected != null;
        }
    }
}
