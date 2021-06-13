using System;
using Xamarin.Forms;
using BetterPractice.BetterMvvm.Navigation;
using System.Threading.Tasks;
using System.Reflection;

namespace BetterPractice.BetterMvvm
{
    public abstract class Application : Xamarin.Forms.Application
    {
        public IoC.Container Container { get; }
        public PageModelMapper PageModelMapper { get; }
        public INavigationService NavigationService { get; }

        private Assembly MainAssembly { get; }

        public Application()
        {
            MainAssembly = Assembly.GetCallingAssembly();
            Container = new IoC.Container();
            PageModelMapper = new PageModelMapper(Container);
            NavigationService = new NavigationService(PageModelMapper);
            RegisterServices();
            RegisterPageModelMappings();
        }

        protected override void OnStart()
        {
            InitialNavigation(Container.Resolve<INavigationService>());
        }

        protected virtual void RegisterServices()
        {
            Container.RegisterSingleton(PageModelMapper);
            Container.RegisterSingleton(NavigationService);
            Container.RegisterTransient<Services.IAlertService, Services.DefaultAlertService>();
        }

        protected virtual void RegisterPageModelMappings()
        {
            PageModelMapper.ScanAssembly(MainAssembly);
        }

        protected abstract Task InitialNavigation(INavigationService navigationService);

    }
}
