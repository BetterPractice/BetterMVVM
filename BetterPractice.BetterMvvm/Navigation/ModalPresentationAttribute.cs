using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BetterPractice.BetterMvvm.Navigation
{
    public class ModalPresentationAttribute : PresentationAttribute
    {
        public override Task ShowPage(Page page)
        {
            if (MainPage == null)
            {
                MainPage = WrapIfNeeded(page);
                return Task.CompletedTask;
            }
            return TopPage!.Navigation.PushModalAsync(WrapIfNeeded(page));
        }

        public override Task DismissPage(Page page)
        {
            var parent = page.Parent as Page;
            if (parent != null)
                return DismissPage(parent);
            return base.DismissPage(page);
        }
    }
}
