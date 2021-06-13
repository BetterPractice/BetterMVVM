using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BetterPractice.BetterMvvm.Navigation
{
    [AttributeUsage(AttributeTargets.Class)]
    public class PresentationAttribute : Attribute, IPresenter, IDismisser
    {
        public static Type DefaultNavPageType = typeof(NavigationPage);

        public Type NavPageType { get; set; }

        protected Page? MainPage
        {
            get => Application.Current.MainPage;
            set => Application.Current.MainPage = value;
        }

        public Page? TopPage => MainPage?.Navigation.ModalStack.LastOrDefault() ?? MainPage;

        public bool ShouldWrapInNav { get; set; }

        public PresentationAttribute(bool shouldWrapInNav = true)
        {
            ShouldWrapInNav = shouldWrapInNav;
            NavPageType = DefaultNavPageType;
        }

        public virtual async Task ShowPage(Page page)
        {
            if (MainPage == null)
            {
                MainPage = new Pages.FadeNavigationMultiPage(WrapIfNeeded(page));
                return;
            }

            var handled = await ShowPage(page, TopPage!);

            if (!handled)
            {
                await TopPage!.Navigation.PushModalAsync(WrapIfNeeded(page));
            }
        }

        protected virtual Task<bool> ShowPage(Page page, Page fromPage)
        {
            if (fromPage is NavigationPage navPage)
            {
                return navPage.PushAsync(page)
                    .ContinueWith(_ => true);
            }
            if (fromPage is TabbedPage tabbedPage)
            {
                if (tabbedPage.CurrentPage == null)
                {
                    tabbedPage.Children.Add(WrapIfNeeded(page));
                    return Task.FromResult(true);
                }
                return ShowPage(page, tabbedPage.CurrentPage);
            }
            if (fromPage is Pages.FadeNavigationMultiPage fadeNav)
            {
                if (fadeNav.CurrentPage == null)
                    return fadeNav.TransitionTo(WrapIfNeeded(page))
                        .ContinueWith(_ => true);
                else
                {
                    return ShowPage(page, fadeNav.CurrentPage);
                }
            }
            return Task.FromResult(false);
        }

        public virtual Task DismissPage(Page page)
        {
            var parent = page.Parent as Page;
            if (parent is NavigationPage navPage)
            {
                if (navPage.Navigation.NavigationStack.Count == 1)
                    return DismissPage(parent);
                if (navPage.Navigation.NavigationStack.Last() == page)
                    return navPage.PopAsync();
                navPage.Navigation.RemovePage(page);
                return Task.CompletedTask;
            }
            if (parent is TabbedPage tabbedPage)
            {
                if (tabbedPage.Children.Count == 1)
                    return DismissPage(parent);
                tabbedPage.Children.Remove(page);
                return Task.CompletedTask;
            }
            if (parent is Pages.FadeNavigationMultiPage)
            {
                return DismissPage(parent);
            }
            if (parent == null)
            {
                if (page == MainPage)
                    DismissRoot();
                if (page == TopPage)
                    return page.Navigation.PopModalAsync();
                throw new InvalidNavigationException("Cannot dismiss dialog that isn't on top.");
            }
            return DismissPage(parent);
        }

        protected virtual Page Wrap(Page page)
        {
            var navPage = (NavigationPage)Activator.CreateInstance(NavPageType, page);
            return navPage;
        }

        protected Page WrapIfNeeded(Page page)
        {
            if (ShouldWrapInNav)
                return Wrap(page);
            return page;
        }

        [DoesNotReturn]
        protected void DismissRoot()
        {
            throw new InvalidNavigationException("Cannot dismiss the root page.");
        }


    }
}
