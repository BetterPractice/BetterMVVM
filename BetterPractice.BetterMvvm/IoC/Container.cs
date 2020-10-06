using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BetterPractice.BetterMvvm.IoC
{
    public class Container : IContainer
    {
        private Dictionary<Type, Func<IContainer, object>> accessors;

        public bool CreateUnknownOnResolve { get; }

        public Container(bool createUnknownOnResolve = true)
        {
            CreateUnknownOnResolve = createUnknownOnResolve;
            accessors = new Dictionary<Type, Func<IContainer, object>>();
        }

        #region Registration Methods

        public void RegisterLazySingleton<TInterface, TObject>() where TObject : TInterface
        {
            accessors[typeof(TInterface)] = container =>
            {
                var obj = container.Create<TObject>()!;
                accessors[typeof(TInterface)] = _ => obj;
                return obj;
            };
        }

        public void RegisterLazySingleton<TInterface>(Func<IContainer, TInterface> factory)
        {
            accessors[typeof(TInterface)] = container =>
            {
                var obj = factory(container)!;
                accessors[typeof(TInterface)] = _ => obj;
                return obj;
            };
        }

        public void RegisterSingleton<TInterface>(TInterface obj)
        {
            accessors[typeof(TInterface)] = _ => obj!;
        }

        public void RegisterSingleton<TInterface, TObject>() where TObject : TInterface
        {
            TInterface obj = Create<TObject>();
            accessors[typeof(TInterface)] = _ => obj!;
        }

        public void RegisterTransient<TInterface, TObject>() where TObject : TInterface
        {
            accessors[typeof(TInterface)] = container => container.Create<TObject>()!;
        }

        public void RegisterTransient<TInterface>(Func<IContainer, TInterface> factory)
        {
            accessors[typeof(TInterface)] = container => factory(container)!;
        }

        #endregion

        #region Resolution and Creation Methods

        public bool CanResolve<TInterface>() => CanResolve(typeof(TInterface));

        public bool CanResolve(Type interfaceType)
        {
            if (accessors.ContainsKey(interfaceType))
                return true;
            if (interfaceType == GetType() || interfaceType == typeof(IContainer))
                return true;
            if (CreateUnknownOnResolve)
                return CanCreate(interfaceType);
            return false;
        }

        public TInterface Resolve<TInterface>() => (TInterface)Resolve(typeof(TInterface));

        public object Resolve(Type interfaceType)
        {
            if (accessors.ContainsKey(interfaceType))
            {
                return accessors[interfaceType].Invoke(this);
            }
            if (interfaceType == GetType() || interfaceType == typeof(IContainer))
            {
                return this;
            }
            if (CreateUnknownOnResolve)
            {
                return Create(interfaceType);
            }
            throw new ArgumentException($"Unregistered type ({interfaceType.FullName}) with `{nameof(CreateUnknownOnResolve)}` set to false.");
        }

        public bool CanCreate<TObject>() => CanCreate(typeof(TObject));

        public bool CanCreate(Type objectType)
        {
            if (objectType.IsAbstract)
                return false;
            if (objectType.IsInterface)
                return false;
            if (objectType.IsGenericType)
                return false;

            return ChooseConstructor(objectType) != null;
        }

        public TObject Create<TObject>() => (TObject)Create(typeof(TObject));

        public object Create(Type objectType)
        {
            var constructor = ChooseConstructor(objectType);
            if (constructor == null)
                throw new ArgumentException($"No constructors found to create object of type {objectType.FullName}.");
            var argList = constructor
                .GetParameters()
                .Select(p => p.ParameterType)
                .Select(p => Resolve(p));
            return constructor.Invoke(argList.ToArray());
        }

        #endregion

        private ConstructorInfo? ChooseConstructor(Type objectType)
        {
            return objectType.GetConstructors(BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance)
                .FirstOrDefault(info => info
                    .GetParameters()
                    .Select(p => p.ParameterType)
                    .All(p => CanResolve(p))
                );
        }


    }
}
