using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace BetterPractice.BetterMvvm.ViewModels
{
    public class ButtonViewModel : ObservableObject
    {
        public ButtonViewModel(
            Func<Task> asyncAction,
            Func<bool>? canExecute = null,
            string? title = null,
            ImageSource? imageSource = null,
            bool isEnabled = true)
        {
            _command = new Command(
                async () =>
                {
                    IsEnabled = false;
                    await asyncAction();
                    IsEnabled = true;
                },
                () => IsEnabled && (canExecute?.Invoke() ?? true));
            _title = title;
            _imageSource = imageSource;
            _isEnabled = isEnabled;
        }

        public ButtonViewModel(
            Action action,
            Func<bool>? canExecute = null,
            string? title = null,
            ImageSource? imageSource = null,
            bool isEnabled = true)
        {
            _command = new Command(
                action,
                () => IsEnabled && (canExecute?.Invoke() ?? true));
            _title = title;
            _imageSource = imageSource;
            _isEnabled = isEnabled;
        }

        private Command _command;
        public ICommand Command
        {
            get => _command;            
        }

        private bool _isEnabled;
        public bool IsEnabled
        {
            get => _isEnabled;
            set
            {
                if (SetProperty(ref _isEnabled, value))
                    _command.ChangeCanExecute();
            }
        }

        private bool _isVisible = true;
        public bool IsVisible
        {
            get => _isVisible;
            set => SetProperty(ref _isVisible, value);
        }

        private string? _title;
        public string? Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        private ImageSource? _imageSource;
        public ImageSource? ImageSource
        {
            get => _imageSource;
            set => SetProperty(ref _imageSource, value);
        }
    }
}
