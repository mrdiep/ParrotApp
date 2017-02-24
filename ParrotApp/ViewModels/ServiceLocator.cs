using System.Collections.Generic;
using Ninject;
using Ninject.Modules;

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
    }

    public class MainInjectModule : NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind<HomeViewModel>().ToSelf();
        }
    }
}