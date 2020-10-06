using System;
using System.Threading.Tasks;
using BetterPractice.BetterMvvm.Navigation;

namespace BetterPractice.BetterMvvm.ViewModels
{
    public interface IPreparable<T>
    {
        void Prepare(T param);
    }

    public class PageModel : ObservableObject
    {
        public INavigationService NavigationService { get; }

        private string? _pageTitle;
        public string? PageTitle
        {
            get => _pageTitle;
            set => SetProperty(ref _pageTitle, value);
        }

        public PageModel(INavigationService navigationService)
        {
            NavigationService = navigationService;
        }

        public virtual Task Initialize() => Task.CompletedTask;

        public virtual void Appearing() { }

        public virtual void Disappearing() { }
    }
}
