using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using BetterPractice.BetterMvvm.Extensions;

namespace BetterPractice.BetterMvvm.Pages
{
    [ContentProperty(nameof(CurrentPage))]
    public class FadeNavigationMultiPage : MultiPage<Page>
    {
        public static readonly BindableProperty CrossFadeProperty = BindableProperty.Create(nameof(CrossFade), typeof(bool), typeof(FadeNavigationMultiPage), false);
        public static readonly BindableProperty AnimationLengthProperty = BindableProperty.Create(nameof(AnimationLength), typeof(uint), typeof(FadeNavigationMultiPage), (uint)250);
        public static readonly BindableProperty AnimationEasingProperty = BindableProperty.Create(nameof(AnimationEasing), typeof(Easing), typeof(FadeNavigationMultiPage), Easing.SinInOut);


        public bool CrossFade
        {
            get => (bool)GetValue(CrossFadeProperty);
            set => SetValue(CrossFadeProperty, value);
        }

        public uint AnimationLength
        {
            get => (uint)GetValue(AnimationLengthProperty);
            set => SetValue(AnimationLengthProperty, value);
        }

        public Easing AnimationEasing
        {
            get => (Easing)GetValue(AnimationEasingProperty);
            set => SetValue(AnimationEasingProperty, value);
        }

        protected override void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();
            Children.RemoveWhere(child => child != CurrentPage);
        }

        public FadeNavigationMultiPage()
        {           
        }

        public FadeNavigationMultiPage(Page page) : base()
        {
            Children.Add(page);
        }

        public async Task TransitionTo(Page page)
        {
            var oldPage = CurrentPage;
            page.Opacity = 0;
            Children.Add(page);
            await TransitionBetween(oldPage, page);
            CurrentPage = page;
            Children.Remove(oldPage);
        }

        protected override Page CreateDefault(object item)
        {
            throw new NotImplementedException();
        }

        protected override void OnChildAdded(Element child)
        {
            base.OnChildAdded(child);
        }

        protected override void LayoutChildren(double x, double y, double width, double height)
        {
            Console.WriteLine($"Layout Triggered with {Children.Count} children.");
            var rect = new Rectangle(x, y, width, height);

            var currentPage = CurrentPage; // local reference

            foreach (var page in Children)
            {
                page.Opacity = page == currentPage ? 1 : 0;
                page.Layout(rect);
            }
        }

        private Task TransitionBetween(Page oldPage, Page newPage)
        {
            var tasks = new List<Task>();

            if (newPage != null)
            {
                tasks.Add(newPage.FadeTo(1, AnimationLength, AnimationEasing));
            }

            if (oldPage != null && CrossFade)
            {
                tasks.Add(oldPage.FadeTo(0, AnimationLength, AnimationEasing));
            }
            return Task.WhenAll(tasks);
        }
    }
}
