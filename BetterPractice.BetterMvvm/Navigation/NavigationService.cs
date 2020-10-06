using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Xamarin.Forms;
using BetterPractice.BetterMvvm.ViewModels;

namespace BetterPractice.BetterMvvm.Navigation
{
    public class NavigationService : INavigationService
    {
        public PageModelMapper PageModelMapper { get; }

        public NavigationService(PageModelMapper pageModelMapper)
        {
            PageModelMapper = pageModelMapper;
        }

        public Task NavigateTo<TPageModel>() where TPageModel : PageModel
        {
            return NavigateInternal<TPageModel>(
                p => AttrubuteForPage(p) ?? new PresentationAttribute());
        }

        public Task NavigateTo<TPageModel, TParam>(TParam param) where TPageModel : PageModel, IPreparable<TParam>
        {
            return NavigateInternal<TPageModel>(
                p => AttrubuteForPage(p) ?? new PresentationAttribute(),
                pm => pm.Prepare(param));
        }

        public Task Close(PageModel pageModel)
        {
            return CloseInternal(pageModel,
                p => p.GetType().GetCustomAttribute<PresentationAttribute>()
                    ?? new PresentationAttribute());
        }

        public Task NavigateToUsing<TPageModel>(IPresenter presenter) where TPageModel : PageModel
        {
            return NavigateInternal<TPageModel>(p => presenter);
        }

        public Task NavigateToUsing<TPageModel, TParam>(TParam param, IPresenter presenter) where TPageModel : PageModel, IPreparable<TParam>
        {
            return NavigateInternal<TPageModel>(
                p => presenter,
                pm => pm.Prepare(param));
        }

        private Task NavigateInternal<TPageModel>(Func<Page, IPresenter> findPresenter, Action<TPageModel>? postCreate = null) where TPageModel : PageModel
        {
            var tasks = new List<Task>();
            var (page, pageModel) = PageModelMapper.CreatePage<TPageModel>();
            var presenter = findPresenter(page);
            postCreate?.Invoke(pageModel);
            tasks.Add(pageModel.Initialize());
            tasks.Add(presenter.ShowPage(page));
            return Task.WhenAll(tasks);
        }


        public Task CloseUsing(PageModel pageModel, IDismisser dismisser)
        {
            return CloseInternal(pageModel, p => dismisser);
        }

        private Task CloseInternal(PageModel pageModel, Func<Page, IDismisser> findDismisser)
        {
            var page = FindPage(pageModel);
            if (page == null)
                return Task.CompletedTask;
            var dismisser = findDismisser(page);
            return dismisser.DismissPage(page);
        }

        public Task SetRoot<TPageModel>() where TPageModel : PageModel
        {
            return NavigateToUsing<TPageModel>(new RootPresentationAttribute());
        }

        public Task SetRoot<TPageModel, TParam>(TParam param) where TPageModel : PageModel, IPreparable<TParam>
        {
            return NavigateToUsing<TPageModel, TParam>(param, new RootPresentationAttribute());
        }

        public Task PresentModal<TPageModel>() where TPageModel : PageModel
        {
            return NavigateToUsing<TPageModel>(new ModalPresentationAttribute());
        }

        public Task PresentModal<TPageModel, TParam>(TParam param) where TPageModel : PageModel, IPreparable<TParam>
        {
            return NavigateToUsing<TPageModel, TParam>(param, new ModalPresentationAttribute());
        }

        public Task DismissModal(PageModel pageModel, IDismisser dismisser)
        {
            return CloseUsing(pageModel, new ModalPresentationAttribute());
        }

        private Page? FindPage(PageModel pageModel)
        {
            var mainPage = Application.Current.MainPage;            
            if (Application.Current.MainPage == null)
                return null;
            return FindPage(pageModel, mainPage)
                ?? mainPage.Navigation.ModalStack
                .Select(p => FindPage(pageModel, p))
                .FirstOrDefault(p => p != null);
        }

        private Page? FindPage(PageModel pageModel, Page page)
        {
            if (page.BindingContext == pageModel)
                return page;
            if (page is NavigationPage navPage)
                return navPage.Navigation.NavigationStack
                    .Select(p => FindPage(pageModel, p))
                    .FirstOrDefault(p => p != null);
            if (page is TabbedPage tabbedPage)
                return tabbedPage.Children
                    .Select(p => FindPage(pageModel, p))
                    .FirstOrDefault(p => p != null);
            return null;
        }

        private PresentationAttribute AttrubuteForPage(Page page)
        {
            var attribute = page.GetType().GetCustomAttribute<PresentationAttribute>() ?? new PresentationAttribute();
            return attribute;
        }
    }
}
