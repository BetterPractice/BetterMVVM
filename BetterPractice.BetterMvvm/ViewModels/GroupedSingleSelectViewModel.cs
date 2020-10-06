using System;
using System.Collections.Generic;

namespace BetterPractice.BetterMvvm.ViewModels
{
    public class GroupedSingleSelectViewModel<TItem, TSection> : ObservableObject, IValidated
        where TItem : class
        where TSection : List<TItem>
    {
        private bool _isValid;
        public bool IsValid
        {
            get => _isValid;
            set => SetProperty(ref _isValid, value);
        }

        public GroupedSingleSelectViewModel()
        {
            _choices = new List<TSection>();
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

        private List<TSection> _choices;
        public List<TSection> Choices
        {
            get => _choices;
            set => SetProperty(ref _choices, value);
        }

        private TItem? _selected;
        public TItem? Selected
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


        protected virtual void OnSelectionChanged(TItem? oldValue)
        {
            IsValid = Selected != null;
        }
    }
}
