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

        public SingleSelectViewModel()
        {
            _choices = new List<T>();
        }

        private string? _header;
        public string? Header
        {
            get => _header;
            set => SetProperty(ref _header, value);
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


        protected virtual void OnSelectionChanged(T? oldValue)
        {
            IsValid = Selected != null;
        }
    }
}
