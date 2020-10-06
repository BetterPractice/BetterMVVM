using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BetterPractice.BetterMvvm.Navigation
{
    public class RootPresentationAttribute : PresentationAttribute
    {
        public override Task ShowPage(Page page)
        {
            MainPage = WrapIfNeeded(page);
            return Task.CompletedTask;
        }

        public override Task DismissPage(Page page)
        {
            DismissRoot();
            return Task.CompletedTask;
        }
    }
}
