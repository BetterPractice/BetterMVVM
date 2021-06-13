using System;
using Xamarin.Forms;

namespace BetterPractice.BetterMvvm.ViewModels
{
    public class RowViewModel : ObservableObject
    {
        private bool _isEnabled;
        public bool IsEnabled
        {
            get => _isEnabled;
            set => SetProperty(ref _isEnabled, value);
        }

        private string? _primaryText;
        public string? PrimaryText
        {
            get => _primaryText;
            set => SetProperty(ref _primaryText, value);
        }

        private string? _secondaryText;
        public string? SecondaryText
        {
            get => _secondaryText;
            set => SetProperty(ref _secondaryText, value);
        }

        private string? _tertiaryText;
        public string? TertiaryText
        {
            get => _tertiaryText;
            set => SetProperty(ref _tertiaryText, value);
        }

        private ImageSource? _image;
        public ImageSource? Image
        {
            get => _image;
            set => SetProperty(ref _image, value);
        }
    }
}
