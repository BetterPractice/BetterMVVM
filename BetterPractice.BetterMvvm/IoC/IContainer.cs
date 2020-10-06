using System;
namespace BetterPractice.BetterMvvm.IoC
{
    public interface IContainer
    {
        bool CanResolve(Type interfaceType);
        bool CanResolve<TInterface>();

        object Resolve(Type interfaceType);
        TInterface Resolve<TInterface>();

        bool CanCreate(Type objectType);
        bool CanCreate<TObject>();

        object Create(Type objectType);
        TObject Create<TObject>();
    }

}
