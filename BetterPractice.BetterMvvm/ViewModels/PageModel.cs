using System;
using System.Threading.Tasks;
using System.Windows.Input;
using BetterPractice.BetterMvvm.Navigation;
using BetterPractice.BetterMvvm.Services;
using Xamarin.Forms;

namespace BetterPractice.BetterMvvm.ViewModels
{
    public interface IPreparable<T>
    {
        void Prepare(T param);
    }

    public enum CloseIndicator
    {
        None,
        Arrow,
        Chevron,
        Cross
    }

    public interface IBusyWorker
    {
        bool IsBusy { get; set; }
    }

    public class PageModel : ObservableObject, IBusyWorker
    {
        public INavigationService NavigationService { get; }
        public IAlertService AlertService { get; }

        private bool _isBusy = false;
        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        private string? _pageTitle;
        public string? PageTitle
        {
            get => _pageTitle;
            set => SetProperty(ref _pageTitle, value);
        }

        private CloseIndicator _closeIndicator = CloseIndicator.None;
        public CloseIndicator CloseIndicator
        {
            get => _closeIndicator;
            set => SetProperty(ref _closeIndicator, value);
        }

        private ICommand _closeCommand;
        public ICommand CloseCommand
        {
            get => _closeCommand;
            set => SetProperty(ref _closeCommand, value);
        }

        public PageModel()
        {
            _closeCommand = new Command(() => Close());
            if (Application.Current is BetterMvvm.Application betterApplicaiton)
            {
                NavigationService = betterApplicaiton.Container.Resolve<INavigationService>();
                AlertService = betterApplicaiton.Container.Resolve<IAlertService>();
            }
            else
            {
                throw new ApplicationException($"Application type must decend from {nameof(betterApplicaiton)}. If that is not possible in this app, do not use this constructor.");
            }
        }

        public PageModel(INavigationService navigationService, IAlertService alertService)
        {
            _closeCommand = new Command(() => Close());
            NavigationService = navigationService;
            AlertService = alertService;
        }

        public virtual Task Initialize() => Task.CompletedTask;

        public virtual void Appearing() { }

        public virtual void Disappearing() { }

        protected virtual Task Close() => NavigationService.Close(this);
    }
}
