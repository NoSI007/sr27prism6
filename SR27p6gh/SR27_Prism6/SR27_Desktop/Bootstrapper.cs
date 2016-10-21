
using System.Windows;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.UnityExtensions;
using Microsoft.Practices.Unity;
using Microsoft.Practices.ServiceLocation;

namespace SR27_Desktop
{
    /// <summary>
    /// Added The Class file and the following:-
    /// 2.1 UnityBootstrapper
    /// 2.2 overide DependencyObject CreateShell()
    /// 2.3 override void InitializeShell()
    /// 2.4 void ConfigureModuleCatalog()
    /// </summary>
    public class Bootstrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return ServiceLocator.Current.GetInstance<Shell>();
            //
            //return new Shell();
        }


        protected override void InitializeShell()
        {
            base.InitializeShell();

            Application.Current.MainWindow = (Window)this.Shell;
            Application.Current.MainWindow.Show();
        }


        protected override void ConfigureModuleCatalog()
        {
            base.ConfigureModuleCatalog();
            // to Add a module we
            // 3.1 
            ModuleCatalog moduleCatalog = (ModuleCatalog)this.ModuleCatalog;
            // 3.2 Add ourr module to the catalog.
            moduleCatalog.AddModule(typeof(SR27Module.SR27Module));

        }

        protected override void ConfigureServiceLocator()
        {

            base.ConfigureServiceLocator();
            ServiceLocator.SetLocatorProvider(() => this.Container.Resolve<IServiceLocator>());
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();
            this.Container.AddNewExtension<UnityBootstrapperExtension>();

            this.Container.RegisterInstance(this.ModuleCatalog);

            //RegisterTypeIfMissing(typeof(IServiceLocator), typeof(UnityServiceLocatorAdapter), true);
            //RegisterTypeIfMissing(typeof(IModuleInitializer), typeof(ModuleInitializer), true);
            //RegisterTypeIfMissing(typeof(IModuleManager), typeof(ModuleManager), true);
            //RegisterTypeIfMissing(typeof(RegionAdapterMappings), typeof(RegionAdapterMappings), true);
            //RegisterTypeIfMissing(typeof(IRegionManager), typeof(RegionManager), true);
            //RegisterTypeIfMissing(typeof(IEventAggregator), typeof(EventAggregator), true);
            //RegisterTypeIfMissing(typeof(IRegionViewRegistry), typeof(RegionViewRegistry), true);
            //RegisterTypeIfMissing(typeof(IRegionBehaviorFactory), typeof(RegionBehaviorFactory), true);


            //ServiceLocator.SetLocatorProvider(() => this.Container.Resolve<IServiceLocator>());

        }
    }



}
