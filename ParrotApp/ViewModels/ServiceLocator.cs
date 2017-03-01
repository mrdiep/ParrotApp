using Ninject;
using Ninject.Modules;
using ParrotApp.Data;
using ParrotApp.Helper;
using System.Collections.Generic;
using Xamarin.Forms;

namespace ParrotApp.ViewModels
{
    public class ServiceLocator
    {
        private StandardKernel Container { get; }

        internal ServiceLocator()
        {
            var standardKernel = new StandardKernel();
            standardKernel.Load(new List<NinjectModule> {
                new MainInjectModule()
            });

            Container = standardKernel;
        }

        public HomeViewModel HomeViewModel => Container.Get<HomeViewModel>();
        public DetailViewModel DetailViewModel => Container.Get<DetailViewModel>();
        public DataConnection DataConnection => Container.Get<DataConnection>();

        public T Get<T>()
        {
            return Container.Get<T>();
        }
    }

    public class MainInjectModule : NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind<HomeViewModel>()
                .ToSelf()
                .InSingletonScope();

            Kernel.Bind<NavigationService>()
                .ToSelf()
                .InSingletonScope();

            Kernel.Bind<DetailViewModel>()
               .ToSelf()
               .InSingletonScope();

            Kernel.Bind<NavigationService>()
                .ToSelf()
                .InSingletonScope();

            Kernel.Bind<DataConnection>()
                .ToSelf()
                .InSingletonScope();

            Kernel.Bind<IFileHelper>()
                .ToMethod(x => DependencyService.Get<IFileHelper>())
                .InSingletonScope();
        }
    }
}