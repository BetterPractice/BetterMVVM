using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using BetterPractice.BetterMvvm.ViewModels;

namespace BetterPractice.BetterMvvm.Navigation
{
    public interface IPresenter
    {
        Task ShowPage(Page page);
    }

    public interface IDismisser
    {
        Task DismissPage(Page page);
    }

    public interface INavigationService
    {
        Task NavigateTo<TPageModel>() where TPageModel : PageModel;
        Task NavigateTo<TPageModel, TParam>(TParam param) where TPageModel : PageModel, IPreparable<TParam>;
        Task Close(PageModel pageModel);

        Task NavigateToUsing<TPageModel>(IPresenter presenter) where TPageModel : PageModel;
        Task NavigateToUsing<TPageModel, TParam>(TParam param, IPresenter presenter) where TPageModel : PageModel, IPreparable<TParam>;
        Task CloseUsing(PageModel pageModel, IDismisser dismisser);

        Task SetRoot<TPageModel>() where TPageModel : PageModel;
        Task SetRoot<TPageModel, TParam>(TParam param) where TPageModel : PageModel, IPreparable<TParam>;

        Task PresentModal<TPageModel>() where TPageModel : PageModel;
        Task PresentModal<TPageModel, TParam>(TParam param) where TPageModel : PageModel, IPreparable<TParam>;

        Task DismissModal(PageModel pageModel, IDismisser dismisser);
    }
}
