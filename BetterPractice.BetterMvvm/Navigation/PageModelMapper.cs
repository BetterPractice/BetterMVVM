using System;
using System.Collections.Generic;
using System.Reflection;
using Xamarin.Forms;
using BetterPractice.BetterMvvm.IoC;

namespace BetterPractice.BetterMvvm.Navigation
{
    public class PageModelMapper
    {
        public IContainer Container { get; }

        public PageModelMapper(IContainer container)
        {
            Container = container;
        }

        private Dictionary<Type, Type> mappings = new Dictionary<Type, Type>();

        public void ScanAssembly(Assembly assembly)
        {
            var attributes = assembly.GetCustomAttributes<PageModelMappingAttribute>();

            foreach (var attribute in attributes)
            {
                RegisterMapping(attribute.PageType, attribute.PageModelType);
            }
        }

        public void RegisterMapping(Type pageType, Type pageModelType)
        {
            mappings[pageModelType] = pageType;
        }

        public void RegisterMapping<TPage, TPageModel>() where TPage : Page where TPageModel : ViewModels.PageModel
        {
            RegisterMapping(typeof(TPage), typeof(TPageModel));
        }

        public (Page, TPageModel) CreatePage<TPageModel>() where TPageModel : ViewModels.PageModel
        {
            var pageModel = Container.Resolve<TPageModel>();
            var pageType = mappings[typeof(TPageModel)];
            var page = (Page)Activator.CreateInstance(pageType);
            page.Appearing += (sender, args) => pageModel.Appearing();
            page.Disappearing += (sender, args) => pageModel.Disappearing();
            page.BindingContext = pageModel;

            return (page, pageModel);
        }
    }
}
