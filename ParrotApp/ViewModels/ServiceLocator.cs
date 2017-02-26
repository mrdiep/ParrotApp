using Ninject;
using Ninject.Modules;
using ParrotApp.Data;
using System.Collections.Generic;

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
        public DataConnection DataConnection => Container.Get<DataConnection>();
    }

    public class MainInjectModule : NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind<HomeViewModel>().ToSelf().InSingletonScope();
            Kernel.Bind<DataConnection>().ToSelf().InSingletonScope();
        }
    }
}