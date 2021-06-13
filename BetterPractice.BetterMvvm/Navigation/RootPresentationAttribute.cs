using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using BetterPractice.BetterMvvm.Pages;

namespace BetterPractice.BetterMvvm.Navigation
{
    public class RootPresentationAttribute : PresentationAttribute
    {
        public uint AnimationLength { get; set; } = 250;
        public Easing AnimationEasing { get; set; } = Easing.SinInOut;

        public override async Task ShowPage(Page page)
        {
            if (MainPage is FadeNavigationMultiPage fadeNav)
            {
                fadeNav.AnimationLength = AnimationLength;
                fadeNav.AnimationEasing = AnimationEasing;
                while (fadeNav.Navigation.ModalStack.Count > 0)
                {
                    await fadeNav.Navigation.PopModalAsync();
                }
                await fadeNav.TransitionTo(WrapIfNeeded(page));
                return;
            }
            if (MainPage == null)
            {
                var root = new FadeNavigationMultiPage(WrapIfNeeded(page))
                {
                    AnimationEasing = AnimationEasing,
                    AnimationLength = AnimationLength
                };
                MainPage = root;
                return;
            }
            var oldMain = MainPage;
            var fade = new FadeNavigationMultiPage(oldMain)
            {
                AnimationEasing = AnimationEasing,
                AnimationLength = AnimationLength
            };
            MainPage = fade;
            await fade.TransitionTo(WrapIfNeeded(page));
            return;
        }

        public override Task DismissPage(Page page)
        {
            DismissRoot();
            return Task.CompletedTask;
        }
    }
}
